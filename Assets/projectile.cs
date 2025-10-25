using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 4f;

    private Rigidbody2D rb;

    void Awake()
    {
        // Přesun z Start() do Awake() - Rigidbody je jistě inicializován
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Rigidbody2D chybí na projektilu!");
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Launch(Vector2 direction, float speed)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D nebyl nalezen!");
            return;
        }
        rb.linearVelocity = direction.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth hp = collision.collider.GetComponent<PlayerHealth>();
            if (hp != null)
                hp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
