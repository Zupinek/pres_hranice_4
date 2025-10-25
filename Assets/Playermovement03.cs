using UnityEngine;

public class Playermovement03 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded = true;

    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Pohyb doleva/doprava
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Animace běhu
        anim.SetBool("isRunning", moveX != 0);

        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("JumpStart", true);  // první fáze skoku
        }

        // Kontrola stavu ve vzduchu
        if (!isGrounded)
        {
            float yVel = rb.linearVelocity.y;

            if (yVel > 0.1f)
            {
                anim.SetBool("JumpUp", true);
                anim.SetBool("JumpDown", false);
            }
            else if (yVel < -0.1f)
            {
                anim.SetBool("JumpUp", false);
                anim.SetBool("JumpDown", true);
            }
        }
        else
        {
            // Reset po dopadu
            anim.SetBool("JumpStart", false);
            anim.SetBool("JumpUp", false);
            anim.SetBool("JumpDown", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
            isGrounded = true;
    }

    // ✅ Funkce pro JumpBoost
    public void ActivateJumpBoost(float boostAmount = 100f)
    {
        jumpForce += boostAmount;
        Debug.Log("JumpBoost aktivován! Nový jumpForce: " + jumpForce);
    }
}
