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

    private void SpawnPickUpWrapper(Vector3 spawnPosition, GameObject supplyItem)
    {
        StartCoroutine(SpawnPickUpCoroutine(spawnPosition, supplyItem));
    }

    private IEnumerator SpawnPickUpCoroutine(Vector3 spawnPosition, GameObject supplyIem)
    {
        yield return new WaitForSeconds(2.2f);
        SpawnPickUp(spawnPosition, supplyIem);
    }

    private void SpawnPickUp(Vector3 spawnPosition, GameObject supplyItem)
    {
        if (itemPrefabs == null)
            itemPrefabs = supplyItem;
        Instantiate(itemPrefabs, spawnPosition + new Vector3(0,2,0), Quaternion.identity);
    }
}
