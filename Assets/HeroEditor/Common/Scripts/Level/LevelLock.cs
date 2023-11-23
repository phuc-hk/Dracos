using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    // This field should be set by the inspector
    [SerializeField] int levelRequirement;

    public void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("level");
        bool levelUnlocked = currentLevel >= levelRequirement;
        GetComponent<Button>().interactable = levelUnlocked;
    }
}
