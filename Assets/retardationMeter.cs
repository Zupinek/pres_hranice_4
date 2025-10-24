using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    [Header("Reference na hráče")]
    public GameObject player;           // Přetáhni objekt hráče (GameObject s pohybem apod.)

    [Header("Nastavení smrti")]
    public float deathDelay = 1.5f;     // Zpoždění před restartem
    public bool autoRestart = true;     // Automaticky restartovat po smrti?
    public bool disablePlayerScripts = true; // Vypne všechny skripty na hráči

    private bool isDead = false;

    /// <summary>
    /// Zavolej tuhle metodu, když má hráč umřít (např. z radiace nebo z HP)
    /// </summary>
    public void OnLethalDoseReached()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("💀 Hráč zemřel!");

        // Deaktivuj hráče nebo jeho skripty
        if (player != null)
        {
            if (disablePlayerScripts)
            {
                MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
                foreach (var script in scripts)
                {
                    script.enabled = false;
                }
            }

            // můžeš místo vypnutí použít: player.SetActive(false);
        }

        // Můžeš přidat efekt (např. ztmavení, zvuk smrti apod.)
        StartCoroutine(RestartScene());
    }

    private System.Collections.IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(deathDelay);

        if (autoRestart)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}
