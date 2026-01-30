using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // Скорость ускорения, настраивается в инспекторе
    public float moveSpeed = 10f;

    // Максимальная скорость движения
    public float maxVelocity = 5f;

    // Плавность торможения при отсутствии ввода
    public float damping = 5f;

    private Rigidbody2D rb;
    private Vector2 input;

    private void Awake()
    {
        // Получаем ссылку на Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = Vector2.zero;

        // Получаем текущее состояние клавиатуры (Input System)
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Обработка WASD и стрелок
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            input.x -= 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            input.x += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            input.y -= 1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            input.y += 1f;

        // Нормализация, чтобы по диагонали скорость была одинаковой
        input = input.normalized;
    }

    private void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            // Плавно ускоряем объект с помощью физики
            rb.AddForce(input * moveSpeed, ForceMode2D.Force);

            // Ограничиваем максимальную скорость
            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
        }
        else
        {
            // Плавное торможение при отсутствии ввода
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, damping * Time.fixedDeltaTime);
        }
    }
}
