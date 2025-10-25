using UnityEngine;

public class JumpBoostPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateJumpBoost();
                Destroy(gameObject); // Zmizí po sebrání
            }
        }
    }
}
