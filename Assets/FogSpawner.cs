using UnityEngine;

public class FogSpawner : MonoBehaviour
{
    [Header("Reference")]
    public Transform player;          // Odkaz na hráče
    public GameObject fogPrefab;      // Prefab mlhy, která se bude spawnovat

    [Header("Nastavení spawnování")]
    public float fogSpacing = 10f;    // Jak daleko od sebe se budou kusy mlhy spawnovat (Y)
    private GameObject lastFog;       // Poslední vytvořená mlha

    private void Start()
    {
        // Na začátku vytvoříme první mlhu, pokud ještě žádná není
        if (lastFog == null && fogPrefab != null)
        {
            lastFog = Instantiate(fogPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (lastFog == null || player == null) return;

        // Pokud hráč dosáhne (nebo překročí) pozici poslední mlhy...
        if (player.position.y >= lastFog.transform.position.y)
        {
            // ...spawni novou mlhu výš (např. o určitou vzdálenost)
            Vector3 spawnPos = lastFog.transform.position + new Vector3(0, fogSpacing, 0);
            lastFog = Instantiate(fogPrefab, spawnPos, Quaternion.identity);
        }
    }
}

