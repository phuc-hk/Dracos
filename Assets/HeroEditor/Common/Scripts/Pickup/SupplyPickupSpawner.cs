using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyPickupSpawner : MonoBehaviour
{
    public GameObject itemPrefabs;

    private void Start()
    {
        EnemyHealth.EnemyDestroyed += SpawnPickUpWrapper;
    }

    private void SpawnPickUpWrapper(Vector3 spawnPosition)
    {
        StartCoroutine(SpawnPickUpCoroutine(spawnPosition));
    }

    private IEnumerator SpawnPickUpCoroutine(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(2.2f);
        SpawnPickUp(spawnPosition);
    }

    private void SpawnPickUp(Vector3 spawnPosition)
    {
        Instantiate(itemPrefabs, spawnPosition + new Vector3(0,2,0), Quaternion.identity);
    }
}
