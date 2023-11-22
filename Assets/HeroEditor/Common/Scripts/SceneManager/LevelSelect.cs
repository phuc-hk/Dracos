using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button selectLevelButtonPrefab;
    public string[] levelNames;
    public float horizontalSpacing = 10f;
    public float verticalSpacing = 20f;

    void Start()
    {
        float x = 0f;
        float y = 0f;

        for (int i = 0; i < levelNames.Length - 1; i++)
        {
            Debug.Log(levelNames[i]);
            Button levelButton = Instantiate(selectLevelButtonPrefab, transform);
            levelButton.GetComponentInChildren<TextMeshProUGUI>().text = levelNames[i];
            levelButton.onClick.AddListener(() => LoadLevelScene(levelNames[i - 1]));

            // Add horizontal spacing
            x += levelButton.GetComponent<RectTransform>().rect.width + horizontalSpacing;

            // Move to next line if out of screen size
            if (x > Screen.width)
            {
                x = 0f;
                y -= levelButton.GetComponent<RectTransform>().rect.height + verticalSpacing;
            }

            // Set the position of the button
            levelButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
    }

    void LoadLevelScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
