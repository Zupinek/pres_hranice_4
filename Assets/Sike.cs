using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public float damage = 20f; // Kolik HP spike ubere

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
