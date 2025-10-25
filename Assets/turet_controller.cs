using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Střelba")]
    public GameObject projectilePrefab;  // Prefab střely
    public Transform firePoint;          // Místo odkud se střílí
    public float fireRate = 1.5f;        // Interval mezi výstřely
    public float projectileSpeed = 8f;   // Rychlost střely
    private float fireTimer;

    [Header("Detekce hráče")]
    public float activationRange = 8f;   // Na jakou vzdálenost se aktivuje
    public float deactivationRange = 12f; // Kdy zmizí, pokud se hráč vzdálí

    private Transform player;
    private bool isActive = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D coll;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        SetTurretState(false); // na začátku vypnutá
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Aktivace a deaktivace turretu
        if (distance <= activationRange && !isActive)
            SetTurretState(true);
        else if (distance > deactivationRange && isActive)
            SetTurretState(false);

        // Pokud je aktivní, střílí v intervalu
        if (isActive)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Fire();
                fireTimer = 0f;
            }
        }
    }

    void Fire()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // Vypočti směr k hráči
        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectile = bullet.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Launch(direction, projectileSpeed);
            
        }

        Debug.Log("Turreta střílí!");
    }

    void SetTurretState(bool state)
    {
        isActive = state;
        if (spriteRenderer != null) spriteRenderer.enabled = state;
        if (coll != null) coll.enabled = state;
    }
}
 