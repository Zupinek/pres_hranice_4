using UnityEngine;
using UnityEngine.UI;

public class LethalDoseSystem : MonoBehaviour
{
    [Header("Reference na radiaci")]
    public RadiationZone2D radiationSystem; // Přetáhni objekt s RadiationZone2D

    [Header("Nastavení smrtelné dávky")]
    public float lethalRate = 4f;       // Jak rychle roste smrtelná dávka
    public float maxLethal = 100f;      // Maximum dávky

    [Header("UI")]
    public Slider lethalSlider;
    public Image fillImage;

    [Header("Barvy přechodu (volitelné)")]
    public Color lowColor = Color.green;
    public Color highColor = Color.black;

    [Header("Reference na systém smrti")]
    public PlayerDeathManager deathManager; // 💀 Přetáhni sem objekt s PlayerDeathManager

    private float currentLethal = 0f;
    private bool lethalReached = false;

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
        if (radiationSystem == null || lethalReached) return;

        // Roste jen pokud je radiace plná
        if (radiationSystem.IsRadiationFull())
            currentLethal += lethalRate * Time.deltaTime;

        // Omez hodnoty
        currentLethal = Mathf.Clamp(currentLethal, 0, maxLethal);
        UpdateUI();

        // Když je dávka plná → informuj PlayerDeathManager
        if (currentLethal >= maxLethal)
        {
            currentLethal = maxLethal;
            lethalReached = true;
            Debug.Log("☢️ Smrtelná dávka dosažena!");
            if (deathManager != null)
                deathManager.OnLethalDoseReached(); // 🧩 zavolá smrt
        }
    }

    void UpdateUI()
    {
        if (lethalSlider == null) return;

        lethalSlider.value = currentLethal;

        if (fillImage != null)
        {
            float t = currentLethal / maxLethal;
            fillImage.color = Color.Lerp(lowColor, highColor, t);
        }
    }

    // Getter (pokud chceš z jiných skriptů číst hodnotu)
    public float GetCurrentLethal() => currentLethal;
}
