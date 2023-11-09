using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CoinSpawnInfo))]
public class CoinSpawnInfoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CoinSpawnInfo spawnInfo = (CoinSpawnInfo)target;

        spawnInfo.shape = (CoinShape)EditorGUILayout.EnumPopup("Shape", spawnInfo.shape);
        spawnInfo.position = (Transform)EditorGUILayout.ObjectField("Position", spawnInfo.position, typeof(Transform), true);

        if (spawnInfo.shape == CoinShape.Rectangle)
        {
            spawnInfo.row = EditorGUILayout.IntField("Row", spawnInfo.row);
            spawnInfo.column = EditorGUILayout.IntField("Column", spawnInfo.column);
            spawnInfo.spacing = EditorGUILayout.FloatField("Spacing", spawnInfo.spacing);
        }
        else if (spawnInfo.shape == CoinShape.Circle)
        {
            spawnInfo.count = EditorGUILayout.IntField("Count", spawnInfo.count);
            spawnInfo.lines = EditorGUILayout.IntField("Lines", spawnInfo.lines);
            spawnInfo.spacing = EditorGUILayout.FloatField("Spacing", spawnInfo.spacing);
        }
        else if (spawnInfo.shape == CoinShape.Heart)
        {
            spawnInfo.count = EditorGUILayout.IntField("Count", spawnInfo.count);
            spawnInfo.spacing = EditorGUILayout.FloatField("Spacing", spawnInfo.spacing);
        }
    }
}
