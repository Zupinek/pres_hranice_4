using UnityEngine;

public class JumpBoostPickup : MonoBehaviour
{
    public float boostAmount = 100f; // kolik zvýší skok

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Playermovement03 player = collision.GetComponent<Playermovement03>();
            if (player != null)
            {
                player.ActivateJumpBoost(boostAmount); // aktivace JumpBoost
                Destroy(gameObject); // předmět zmizí
            }
        }
    }
}
