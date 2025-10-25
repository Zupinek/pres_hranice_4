using UnityEngine;

public class PortalQuit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrola, jestli se dotkl hráč
        if (collision.CompareTag("Player"))
        {
            Debug.Log("🎉 Hráč vstoupil do portálu! Hra se vypíná...");

#if UNITY_EDITOR
            // Pokud běžíš v Unity Editoru, vypne Play mód
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // Pokud je to build (exe, WebGL, apod.) – hra se zavře
            Application.Quit();
#endif
        }
    }
}

