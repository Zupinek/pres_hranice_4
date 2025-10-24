using UnityEngine;



public class mlhaUp : MonoBehaviour
{
    public float moveDistance = 1f;   // O kolik jednotek se má objekt posunout nahoru
    public float interval = 0.1f;       // Jak často (v sekundách) se má pohybovat

    private void Start()
    {
        // Spustí opakované volání metody MoveUp každou sekundu
        InvokeRepeating(nameof(MoveUp), interval, interval);
    }

    private void MoveUp()
    {
        transform.position += Vector3.up * moveDistance;
    }
}

