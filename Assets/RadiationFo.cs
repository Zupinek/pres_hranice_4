using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RadiationFog : MonoBehaviour
{
    public float damagePerSecond = 10f;   // kolik DMG za sekundu
    public Color fogColor = new Color(0.5f, 1f, 0.5f, 0.3f); // nazelenalá mlha
    public GameObject fogVisualPrefab;    // volitelně prefab pro mlhu

    private void Start()
    {
        // Nastav collider jako trigger
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;

        // Automaticky vytvoří mlhový efekt (pokud není)
        if (fogVisualPrefab != null)
        {
            GameObject fog = Instantiate(fogVisualPrefab, transform);
            fog.transform.localPosition = Vector3.zero;
            fog.transform.localScale = Vector3.one;
        }
        else
        {
            // Pokud není prefab, vytvoří jednoduchý sprite
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.color = fogColor;
            sr.sortingOrder = -5;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Pokud má hráč skript na zdraví, aplikuj poškození
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
