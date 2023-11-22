using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadLevelScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
