using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinShape
{
    Grid,
    Circle,
    Triangle,
    Rainbow
}

[System.Serializable]
public class CoinSpawnInfo
{
    public Transform position;
    public CoinShape shape;
    public int count;
    public float spacing;
    public float radius;
}

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public List<CoinSpawnInfo> spawnInfos;

    private void Start()
    {
        foreach (CoinSpawnInfo spawnInfo in spawnInfos)
        {
            switch (spawnInfo.shape)
            {
                case CoinShape.Grid:
                    SpawnCoinsInGrid(spawnInfo.position, spawnInfo.count, spawnInfo.spacing);
                    break;
                case CoinShape.Circle:
                    SpawnCoinsInCircle(spawnInfo.position, spawnInfo.count, (int)spawnInfo.radius, spawnInfo.spacing);
                    break;
                //case CoinShape.Triangle:
                //    SpawnCoinsInTriangle(spawnInfo.position, spawnInfo.count, spawnInfo.spacing);
                //    break;
                //case CoinShape.Rainbow:
                //    SpawnCoinsInRainbow(spawnInfo.position, spawnInfo.count, spawnInfo.spacing, spawnInfo.radius);
                //    break;
                default:
                    Debug.LogError("Invalid shape: " + spawnInfo.shape);
                    break;
            }
        }

    }

    private void SpawnCoinsInGrid(Transform position, int count, float spacing)
    {
        int rows = Mathf.CeilToInt(Mathf.Sqrt(count));
        int columns = Mathf.CeilToInt((float)count / rows);

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns && row * columns + column < count; column++)
            {
                Vector3 offset = new Vector3(column * spacing, row * spacing, 0f);
                Vector3 coinPosition = position.position + offset;
                Instantiate(coinPrefab, coinPosition, Quaternion.identity, position);
            }
        }
    }


    public void SpawnCoinsInCircle(Transform centerPosition, int coinsPerLine, int lines, float space)
    {
        float angle = 180f / lines;
        float sub;
        if (coinsPerLine % 2 != 0)
            sub = coinsPerLine / 2;
        else
            sub = coinsPerLine / 2 - 0.5f;
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < coinsPerLine; j++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * (angle * i)) * (j - sub) * space;
                float y = Mathf.Cos(Mathf.Deg2Rad * (angle * i)) * (j - sub) * space;
                Vector3 position = new Vector3(x, y, 0) + centerPosition.position;
                Instantiate(coinPrefab, position, Quaternion.identity, centerPosition);
            }
        }
    }
}