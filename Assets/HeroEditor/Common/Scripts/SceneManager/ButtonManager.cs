using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Sprite playImage;
    [SerializeField] Sprite pauseImage;
    public void LoadLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadLevelScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void PauseButton(Button button)
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            button.GetComponent<Image>().sprite = playImage;
        }
        else
        {
            Time.timeScale = 1;
            button.GetComponent<Image>().sprite = pauseImage;
        }    
                  
    }
}
