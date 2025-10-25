using UnityEngine;

public class BackgroundFollowXY : MonoBehaviour
{
    public Transform player;        // Hráč, kterého bude pozadí sledovat
    public float followSpeed = 2f;  // Rychlost, jakou pozadí dohání hráče
    public Vector2 offset = Vector2.zero; // Posun pozadí vůči hráči (X, Y)

    private void LateUpdate()
    {
        // Cílová pozice pozadí
        Vector3 targetPosition = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            transform.position.z
        );

        // Plynulé přesouvání směrem k hráči
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
