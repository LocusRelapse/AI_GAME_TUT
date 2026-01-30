using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // Обычное ускорение движения
    public float moveSpeed = 8f;

    // Максимальная скорость обычного движения
    public float maxVelocity = 4f;

    // Плавность торможения
    public float damping = 10f;

    // Ускорение во время рывка
    public float dashForce = 30f;

    // Длительность рывка в секундах
    public float dashDuration = 1f;

    // Кулдаун рывка
    public float dashCooldown = 2f;

    private Rigidbody2D rb;
    private Vector2 input;

    // Флаги и таймеры рывка
    private bool isDashing = false;
    private float dashEndTime;
    private float lastDashTime;

    private void Awake()
    {
        // Получаем Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = Vector2.zero;

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Считываем направление движения
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            input.x -= 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            input.x += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            input.y -= 1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            input.y += 1f;

        // Нормализуем ввод
        input = input.normalized;

        // Нажатие Shift — попытка начать рывок
        if ((keyboard.leftShiftKey.wasPressedThisFrame ||
             keyboard.rightShiftKey.wasPressedThisFrame))
        {
            TryStartDash();
        }

        // Проверяем окончание рывка по времени
        if (isDashing && Time.time >= dashEndTime)
        {
            isDashing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            // Во время рывка постоянно прикладываем сильное ускорение
            rb.AddForce(input * dashForce, ForceMode2D.Force);
            return;
        }

        if (input != Vector2.zero)
        {
            // Обычное плавное движение
            rb.AddForce(input * moveSpeed, ForceMode2D.Force);

            // Ограничиваем максимальную скорость
            if (rb.linearVelocity.magnitude > maxVelocity)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
            }
        }
        else
        {
            // Плавная остановка
            rb.linearVelocity = Vector2.Lerp(
                rb.linearVelocity,
                Vector2.zero,
                damping * Time.fixedDeltaTime
            );
        }
    }

    private void TryStartDash()
    {
        // Нельзя начать рывок без направления
        if (input == Vector2.zero) return;

        // Проверяем кулдаун
        if (Time.time - lastDashTime < dashCooldown) return;

        // Запускаем рывок
        isDashing = true;
        lastDashTime = Time.time;
        dashEndTime = Time.time + dashDuration;

        // Сбрасываем текущую скорость, чтобы рывок был отчетливым
        rb.linearVelocity = Vector2.zero;
    }
}
