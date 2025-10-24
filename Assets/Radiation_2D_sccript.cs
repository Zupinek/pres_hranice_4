using UnityEngine;
using UnityEngine.UI;

public class RadiationZone2D : MonoBehaviour
{
    [Header("Nastavení radiace")]
    public float radiationRate = 5f;     // přibývání radiace za sekundu
    public float maxRadiation = 100f;    // maximum radiace
    public float decayRate = 2f;         // klesání radiace mimo zónu

    private bool isInZone = false;
    private float currentRadiation = 0f;

    [Header("UI")]
    public Slider radiationSlider;     // Slider v UI
    public Image fillImage;            // obrázek uvnitř slideru (Fill)

    [Header("Barvy přechodu")]
    public Color safeColor = Color.green;     // barva při 0 radiaci
    public Color mediumColor = Color.yellow;  // barva při střední radiaci
    public Color dangerColor = Color.red;     // barva při max radiaci

    void Start()
    {
        // Nastavení slideru
        if (radiationSlider != null)
        {
            radiationSlider.minValue = 0;
            radiationSlider.maxValue = maxRadiation;
            radiationSlider.value = 0;
        }
    }

    void Update()
    {
        // 🔹 Růst nebo pokles radiace
        if (isInZone)
            currentRadiation += radiationRate * Time.deltaTime;
        else
            currentRadiation -= decayRate * Time.deltaTime;

        // Udržení hodnoty v rozsahu
        currentRadiation = Mathf.Clamp(currentRadiation, 0, maxRadiation);

        // 🔹 Aktualizace UI
        UpdateRadiationBar();

        // 🔹 Co se stane při max radiaci
        if (currentRadiation >= maxRadiation)
        {
            Debug.Log("☢️ Hráč dostal smrtelnou dávku radiace!");
            // Zde můžeš volat funkci na smrt hráče
        }
    }

    private void UpdateRadiationBar()
    {
        if (radiationSlider == null) return;

        // Nastavení hodnoty lišty
        radiationSlider.value = currentRadiation;

        // Výpočet poměru 0–1
        float t = currentRadiation / maxRadiation;

        // Přechod barev (zelená → žlutá → červená)
        Color barColor;
        if (t < 0.5f)
            barColor = Color.Lerp(safeColor, mediumColor, t * 2f);
        else
            barColor = Color.Lerp(mediumColor, dangerColor, (t - 0.5f) * 2f);

        // Nastavení barvy výplně slideru
        if (fillImage != null)
            fillImage.color = barColor;
    }

    // Trigger 2D vstup
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isInZone = true;
    }

    // Trigger 2D výstup
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isInZone = false;
    }
}
