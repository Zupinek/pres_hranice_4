using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Jump Boost Settings")]
    public float jumpBoostMultiplier = 2f;  // Jak moc silnější skok
    public float boostDuration = 5f;        // Jak dlouho vydrží boost
    public Image jumpBoostIcon;             // UI ikona (dáš do Inspectoru)

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool hasBoost = false;
    private float boostTimer = 0f;
    private float defaultJumpForce;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        defaultJumpForce = jumpForce;

        if (jumpBoostIcon != null)
            jumpBoostIcon.enabled = false;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleBoostTimer();
    }

    private void HandleMovement()
    {
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleBoostTimer()
    {
        if (!hasBoost) return;

        boostTimer -= Time.deltaTime;
        if (boostTimer <= 0)
        {
            jumpForce = defaultJumpForce;
            hasBoost = false;

            if (jumpBoostIcon != null)
                jumpBoostIcon.enabled = false;
        }
    }

    public void ActivateJumpBoost()
    {
        hasBoost = true;
        boostTimer = boostDuration;
        jumpForce = defaultJumpForce * jumpBoostMultiplier;

        if (jumpBoostIcon != null)
            jumpBoostIcon.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = false;
    }
}
