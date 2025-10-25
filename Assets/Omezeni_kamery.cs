using UnityEngine;

public class CameraFollowWithLimits : MonoBehaviour
{
    public Transform target;      // Hráč nebo objekt, který kamera sleduje
    public float smoothSpeed = 5f;
    public Vector2 minLimits;     // Minimální X a Y pozice kamery
    public Vector2 maxLimits;     // Maximální X a Y pozice kamery

    void LateUpdate()
    {
        if (target == null) return;

        // Cíl, kam má kamera jít (s hladkým přechodem)
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Omezení kamery do daných hranic
        float clampedX = Mathf.Clamp(smoothedPosition.x, minLimits.x, maxLimits.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minLimits.y, maxLimits.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // Volitelné – pro vizuální kontrolu hranic v editoru
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = new Vector3((minLimits.x + maxLimits.x) / 2, (minLimits.y + maxLimits.y) / 2, 0);
        Vector3 size = new Vector3(maxLimits.x - minLimits.x, maxLimits.y - minLimits.y, 0);
        Gizmos.DrawWireCube(center, size);
    }
}
