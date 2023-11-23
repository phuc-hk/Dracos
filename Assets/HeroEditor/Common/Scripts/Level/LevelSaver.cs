using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{  
    void Start()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            SaveLevel(1);
        }
        //DontDestroyOnLoad(gameObject);
    }

    // This function should be called through the button's OnClick
    public void SaveLevel(int id)
    {
        PlayerPrefs.SetInt("level", id);
    }

}
