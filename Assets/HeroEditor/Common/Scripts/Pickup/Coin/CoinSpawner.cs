using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum CoinShape
{
    Grid,
    Circle,
    Triangle,
    Rainbow,
    Rectangle, 
    Heart
}

//[System.Serializable]


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
                    SpawnCoinsInCircle(spawnInfo.position, spawnInfo.count, spawnInfo.lines, spawnInfo.spacing);
                    break;
                case CoinShape.Rectangle:
                    SpawnCoinsInRectangle(spawnInfo.position, spawnInfo.row, spawnInfo.column, spawnInfo.spacing);
                    break;
                case CoinShape.Heart:
                    SpawnCoinsInHeart(spawnInfo.position, spawnInfo.count, spawnInfo.spacing);
                    break;
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

    public void SpawnCoinsInRectangle(Transform position, int row, int column, float space)
    {
        float startX = position.position.x - ((column - 1) * space) / 2;
        float startY = position.position.y + ((row - 1) * space) / 2;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Vector3 spawnPosition = new Vector3(startX + j * space, startY - i * space, position.position.z);
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity, position);
            }
        }
    }

    private void SpawnCoinsInHeart(Transform position, int count, float spacing)
    {
        float radius = 0.1f;
        float angle = 0f;
        float angleStep = 0.3f;
        float x, y;

        for (int i = 0; i < count; i++)
        {
            angle += angleStep;
            x = radius * 16 * Mathf.Pow(Mathf.Sin(angle), 3);
            y = (-radius * (13 * Mathf.Cos(angle) - 5 * Mathf.Cos(2 * angle) - 2 * Mathf.Cos(3 * angle) - Mathf.Cos(4 * angle))) * -1;
            Vector3 spawnPosition = new Vector3(x, y, 0f) * spacing;
            Instantiate(coinPrefab, position.position + spawnPosition, Quaternion.identity, position);

            // Spawn a coin at the reflected position to create the other half of the heart
            Vector3 reflectedSpawnPosition = new Vector3(-x, y, 0f) * spacing;
            Instantiate(coinPrefab, position.position + reflectedSpawnPosition, Quaternion.identity, position);
        }
    }
}