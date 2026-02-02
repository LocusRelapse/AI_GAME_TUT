using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float maxVelocity = 4f;
    public float damping = 10f;

    public float dashForce = 30f;
    public float dashDuration = 1f;
    public float dashCooldown = 2f;

    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 input;

    private bool isDashing;
    private float dashEndTime;
    private float lastDashTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
{
    input = Vector2.zero;
    var kb = Keyboard.current;
    if (kb == null) return;

    if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) input.x = -1;
    if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) input.x = 1;
    if (kb.wKey.isPressed || kb.upArrowKey.isPressed) input.y = 1;
    if (kb.sKey.isPressed || kb.downArrowKey.isPressed) input.y = -1;

    input = input.normalized;

    // ---------- ANIMATOR ----------
    animator.SetBool("IsMoving", input != Vector2.zero);
    animator.SetFloat("VelocityX", input.x);
    animator.SetFloat("VelocityY", input.y);

    // ---------- FLIP ТОЛЬКО ДЛЯ SIDE ----------
    if (input.x != 0 && Mathf.Abs(input.y) < 0.01f)
    {
        spriteRenderer.flipX = input.x < 0;
    }
}


    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.AddForce(input * dashForce, ForceMode2D.Force);
            return;
        }

        if (input != Vector2.zero)
        {
            rb.AddForce(input * moveSpeed, ForceMode2D.Force);
            if (rb.linearVelocity.magnitude > maxVelocity)
                rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(
                rb.linearVelocity,
                Vector2.zero,
                damping * Time.fixedDeltaTime
            );
        }
    }

    private void TryStartDash()
    {
        if (input == Vector2.zero) return;
        if (Time.time - lastDashTime < dashCooldown) return;

        isDashing = true;
        lastDashTime = Time.time;
        dashEndTime = Time.time + dashDuration;
        rb.linearVelocity = Vector2.zero;
    }
}