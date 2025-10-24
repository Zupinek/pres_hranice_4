using UnityEngine;

public class VerticalFollowCamera : MonoBehaviour
{
    public Camera mainCamera;
    public float parallaxFactor = 0.5f; // 0 = statický, 1 = plně sleduje kameru
    private Vector3 startPosition;
    private float startCameraY;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        startPosition = transform.position;
        startCameraY = mainCamera.transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 newPos = startPosition;
        float cameraDeltaY = mainCamera.transform.position.y - startCameraY;
        newPos.y += cameraDeltaY * parallaxFactor;
        transform.position = newPos;
    }
}
