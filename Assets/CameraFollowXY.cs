using UnityEngine;

public class CameraFollowXY : MonoBehaviour
{
    public Transform player; // Přetáhneš hráče do tohohle v Inspectoru
    public float smoothSpeed = 5f;
    public Vector2 offset = Vector2.zero; // Offset pro X i Y osu

    private void LateUpdate()
    {
        // Cílová pozice kamery sleduje hráče na X a Y, s offsetem
        Vector3 targetPosition = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            transform.position.z
        );

        // Plynulé přecházení kamery na novou pozici
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
