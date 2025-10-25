using UnityEngine;

public class BackgroundFollowGroup : MonoBehaviour
{
    public Transform target; // hráč nebo kamera
    public float smoothSpeed = 5f;
    public Vector2 offset = Vector2.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
