using UnityEngine;

public class ParallaxBackgroundGroup : MonoBehaviour
{
    public Transform cameraTransform;   // kamera
    public float parallaxMultiplier = 0.5f; // menší číslo = pomalejší pohyb (vzdálenější pozadí)

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, deltaMovement.y * parallaxMultiplier, 0);
        lastCameraPosition = cameraTransform.position;
    }
}


