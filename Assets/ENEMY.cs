using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [Header("Cíl")]
    public Transform player;

    [Header("Pohyb")]
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public float detectionRadius = 6f;

    [Header("Detekce země")]
    public Transform groundCheck;        // prázdný objekt pod nohama
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;        // nastav v Inspectoru na vrstvu země

    [Header("Útok")]
    public float damage = 10f;
    public float attackInterval = 1.0f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject pObj = GameObject.FindGameObjectWithTag("Player");
            if (pObj != null) player = pObj.transform;
        }
    }

    void Update()
    {
        CheckGround();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            float directionX = Mathf.Sign(player.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(directionX * moveSpeed, rb.linearVelocity.y);

            // ✅ Skok jen pokud je na zemi a hráč výš
            if (isGrounded && player.position.y > transform.position.y + 0.5f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }

            // Otočení směrem k hráči
            Vector3 scale = transform.localScale;
            scale.x = directionX * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    // Damage přes čas
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastAttackTime >= attackInterval)
            {
                PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(damage);
                    Debug.Log($"☠️ Enemy způsobil hráči {damage} poškození");
                }
                lastAttackTime = Time.time;
            }
        }
    }

    void CheckGround()
    {
        // ✅ Raycast dolů pro přesnou detekci
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
    }

    void OnDrawGizmosSelected()
    {
        // detekční radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // ray pro zemi
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}
