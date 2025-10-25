using UnityEngine;

public class WinZoneQuit : MonoBehaviour
{
    [Header("Souřadnice pro výhru")]
    public Vector2 targetPosition; // X a Y souřadnice, kde hráč vyhraje
    public float tolerance = 0.5f; // jak přesně musí hráč dosáhnout pozice

    [Header("Reference")]
    public GameObject player; // Hráč

    private bool hasWon = false;

    private void Update()
    {
        if (hasWon || player == null) return;

        Vector2 playerPos = player.transform.position;

        if (Mathf.Abs(playerPos.x - targetPosition.x) <= tolerance &&
            Mathf.Abs(playerPos.y - targetPosition.y) <= tolerance)
        {
            Win();
        }
    }

    private void Win()
    {
        hasWon = true;
        Debug.Log("Vyhrál jsi! 🎉");

        // Ukončení hry
#if UNITY_EDITOR
        // Pokud jsi v editoru, zastaví Play mód
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Pokud je build, hra se zavře
        Application.Quit();
#endif
    }
}
