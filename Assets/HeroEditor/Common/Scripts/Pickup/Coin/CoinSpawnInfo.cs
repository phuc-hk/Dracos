using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CoinSpawnInfo", menuName = "CoinSpawnInfo", order = 1)]
public class CoinSpawnInfo : ScriptableObject
{
    public CoinShape shape;
    public Transform position;
    [Tooltip("Number of object per diameter")]
    public int count;
    [Tooltip("Number of diameter")]
    public int lines;
    [Tooltip("Number of row")]
    public int row;
    [Tooltip("Number of column")]
    public int column;
    [Tooltip("Space between 2 coins")]
    public float spacing;

}