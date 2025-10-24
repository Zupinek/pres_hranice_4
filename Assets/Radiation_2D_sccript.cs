using UnityEngine;
using UnityEngine.UI;

public class RadiationZone2D : MonoBehaviour
{
    [Header("Nastavení radiace")]
    public float radiationRate = 5f;      // Přibývání radiace za sekundu
    public float maxRadiation = 100f;     // Maximum radiace
    public float decayRate = 2f;          // Klesání radiace mimo zónu

    [Header("UI")]
    public Slider radiationSlider;        // Slider pro radiaci
    public Image fillImage;               // Fill uvnitř slideru

    [Header("Barvy")]
    public Color safeColor = Color.green;
    public Color dangerColor = Color.red;

    private bool isInZone = false;
    private float currentRadiation = 0f;

    void Start()
    {
        if (radiationSlider != null)
        {
            radiationSlider.minValue = 0;
            radiationSlider.maxValue = maxRadiation;
            radiationSlider.value = 0;
        }
    }

    void Update()
    {
        UpdateRadiation();
        UpdateUI();
    }

    void UpdateRadiation()
    {
        if (isInZone)
            currentRadiation += radiationRate * Time.deltaTime;
        else
            currentRadiation -= decayRate * Time.deltaTime;

        currentRadiation = Mathf.Clamp(currentRadiation, 0, maxRadiation);
    }

    void UpdateUI()
    {
        if (radiationSlider == null) return;
if (currentRadiation >= maxRadiation - 0.01f)
{
    currentRadiation = maxRadiation;
}
        radiationSlider.value = currentRadiation;

        float t = currentRadiation / maxRadiation;
        if (fillImage != null)
            fillImage.color = Color.Lerp(safeColor, dangerColor, t);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isInZone = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isInZone = false;
    }

    // 🔹 Getter pro LethalDoseSystem
    public bool IsRadiationFull()
    {
        return currentRadiation >= maxRadiation - 0.01f;
    }
}
