using UnityEngine;
using System.Collections;

public class MlhaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mlhaPrefab; // Prefab mlhy (Mlha003_0)
    [SerializeField] private float spawnInterval = 10f; // čas mezi spawny
    [SerializeField] private Vector3 spawnPosition = new Vector3(-3.86147f, -10.14f, 0f); // souřadnice spawnu

    private void Start()
    {
        // Spustí opakované spawnování
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnMlha();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMlha()
    {
        Instantiate(mlhaPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawnuta Mlha003_0 v čase: " + Time.time);
    }
}
