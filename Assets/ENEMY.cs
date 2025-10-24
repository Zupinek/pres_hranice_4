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
    public LayerMask groundLayer;        // nastav v Inspectoru

    [Header("Útok")]
    public float damage = 10f;
    public float attackInterval = 1.0f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastAttackTime;
    private float lastJumpTime;
    public float jumpCooldown = 0.5f; // 🕒 zabrání opakovaným skokům

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

        // 🧠 Debug info – uvidíš v konzoli, kdy enemy myslí, že je na zemi
        // Debug.Log($"{gameObject.name} grounded = {isGrounded}");
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            float directionX = Mathf.Sign(player.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(directionX * moveSpeed, rb.linearVelocity.y);

            // ✅ Skok jen pokud:
            // - Je na zemi
            // - Hráč je výš
            // - Uplynul cooldown
            if (isGrounded && player.position.y > transform.position.y + 0.5f && Time.time - lastJumpTime > jumpCooldown)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                lastJumpTime = Time.time;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}
