using UnityEngine;
using UnityEngine.UI;

public class LethalDoseSystem : MonoBehaviour
{
    [Header("Reference na radiaci")]
    public RadiationZone2D radiationSystem; // Přetáhni objekt se skriptem RadiationZone2D

    [Header("Nastavení smrtelné dávky")]
    public float lethalRate = 4f;      // Jak rychle roste smrtelná dávka
    public float maxLethal = 100f;     // Maximum smrtelné dávky

    [Header("UI")]
    public Slider lethalSlider;        // Slider pro smrtelnou dávku
    public Image fillImage;            // Fill Image uvnitř slideru (volitelné)

    [Header("Barvy přechodu (volitelné)")]
    public Color lowColor = Color.green;
    public Color highColor = Color.black;

    private float currentLethal = 0f;

    void Start()
    {
        if (lethalSlider != null)
        {
            lethalSlider.minValue = 0;
            lethalSlider.maxValue = maxLethal;
            lethalSlider.value = 0;
            lethalSlider.wholeNumbers = false; // ✅ přesnější výpočet bez zaokrouhlení
        }
    }

    void Update()
    {
        if (radiationSystem == null) return;

        // 🔹 Roste jen pokud je radiace plná (nikdy neklesá)
        if (radiationSystem.IsRadiationFull())
        {
            currentLethal += lethalRate * Time.deltaTime;
        }

        // 🔹 Udržení hodnoty v rozsahu
        currentLethal = Mathf.Clamp(currentLethal, 0, maxLethal);

        // 🔹 Aktualizace UI
        UpdateUI();

        // 🔹 Kontrola maximální dávky
        if (currentLethal >= maxLethal)
        {
            Debug.Log("💀 Hráč dostal smrtelnou dávku radiace!");
            // TODO: Zde můžeš přidat smrt, efekt, respawn apod.
        }
    }

    void UpdateUI()
    {
        if (lethalSlider == null) return;

        // ✅ Dorovnání – zajistí, že slider je vizuálně úplně plný
        if (currentLethal >= maxLethal - 0.001f)
        {
            currentLethal = maxLethal;
            lethalSlider.value = lethalSlider.maxValue;
        }
        else
        {
            lethalSlider.value = currentLethal;
        }

        // 🔹 Přechod barvy (volitelné)
        if (fillImage != null)
        {
            float t = currentLethal / maxLethal;
            fillImage.color = Color.Lerp(lowColor, highColor, t);
        }
    }

    // 🔸 Volitelné – zjistí, jestli je dávka plná
    public bool IsLethalFull()
    {
        return currentLethal >= maxLethal;
    }
}
