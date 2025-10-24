using UnityEngine;


public class Playermovement03 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Pohyb doleva/doprava
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Když dopadne na zem, může znovu skákat
        isGrounded = true;
    }
}
