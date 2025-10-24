using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    public Transform player; // Přetáhneš hráče do tohohle v Inspectoru
    public float smoothSpeed = 5f;
    public float offsetY = 0f; // Můžeš si posunout kameru nahoru/dolů

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}