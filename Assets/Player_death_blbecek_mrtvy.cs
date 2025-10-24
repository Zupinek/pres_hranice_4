using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    [Header("Nastavení hráče")]
    public GameObject player;          // Přetáhni objekt hráče
    public float deathDelay = 1.5f;    // Kolik sekund po smrti se restartuje
    public bool autoRestart = true;    // Pokud nechceš restart, vypni

    private bool isDead = false;

    public void OnLethalDoseReached()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("💀 Hráč zemřel na radiaci!");
        StartCoroutine(HandleDeath());
    }

    private System.Collections.IEnumerator HandleDeath()
    {
        // Vypni ovládání hráče (všechny MonoBehaviour skripty)
        if (player != null)
        {
            var scripts = player.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
                script.enabled = false;
        }

        // Tady můžeš přidat animaci, fade efekt, zvuk smrti atd.
        yield return new WaitForSeconds(deathDelay);

        if (autoRestart)
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
    }
}
