using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LethalDoseSystem : MonoBehaviour
{
    [Header("Reference na radiaci")]
    public RadiationZone2D radiationSystem; // přetáhni objekt s RadiationZone2D

    [Header("Nastavení smrtelné dávky")]
    public float lethalRate = 5f;
    public float maxLethal = 100f;

    [Header("UI")]
    public Slider lethalSlider;
    public Image fillImage;
    public Color lowColor = Color.green;
    public Color highColor = Color.black;

    [Header("Smrt hráče")]
    public GameObject player;          // 🔥 přetáhni objekt hráče
    public float deathDelay = 1.5f;    // zpoždění restartu po smrti
    public bool autoRestart = true;

    private float currentLethal = 0f;
    private bool isDead = false;

    void Start()
    {
        if (lethalSlider != null)
        {
            lethalSlider.minValue = 0;
            lethalSlider.maxValue = maxLethal;
            lethalSlider.value = 0;
            lethalSlider.wholeNumbers = false;
        }
    }

    void Update()
    {
        if (radiationSystem == null || isDead) return;

        // Přibývání smrtelné dávky
        if (radiationSystem.IsRadiationFull())
            currentLethal += lethalRate * Time.deltaTime;

        currentLethal = Mathf.Clamp(currentLethal, 0, maxLethal);
        UpdateUI();

        // Smrt
        if (currentLethal >= maxLethal && !isDead)
        {
            isDead = true;
            Debug.Log("☢️ Hráč zemřel na radiaci!");
            StartCoroutine(HandleDeath());
        }
    }

    void UpdateUI()
    {
        if (lethalSlider == null) return;

        // slider se dorovná přesně na konec
        if (currentLethal >= maxLethal - 0.001f)
        {
            currentLethal = maxLethal;
            lethalSlider.value = lethalSlider.maxValue;
        }
        else
        {
            lethalSlider.value = currentLethal;
        }

        if (fillImage != null)
        {
            float t = currentLethal / maxLethal;
            fillImage.color = Color.Lerp(lowColor, highColor, t);
        }
    }

    System.Collections.IEnumerator HandleDeath()
    {
        // vypne všechny skripty na hráči
        if (player != null)
        {
            MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
            foreach (var s in scripts)
            {
                s.enabled = false;
            }
        }

        // můžeš sem přidat animaci smrti nebo efekt
        yield return new WaitForSeconds(deathDelay);

        if (autoRestart)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}
