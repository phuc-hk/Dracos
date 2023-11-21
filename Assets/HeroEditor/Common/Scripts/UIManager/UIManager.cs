using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject guestPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject instructionPanel;
    public Health health;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    private void OnEnable()
    {
        health.OnDie.AddListener(ShowLosePanel);
    }

    private void OnDisable()
    {
        health.OnDie.RemoveAllListeners();
    }

    void Start()
    {
        guestPanel.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void ShowGuestPanel()
    {
        Time.timeScale = 0;
        guestPanel.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        StartCoroutine(PauseWithDelay(2));
        guestPanel.SetActive(false);
        losePanel.SetActive(true);
        winPanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        StartCoroutine(PauseWithDelay(2));
        guestPanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void Close()
    {
        guestPanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowInstructionPanel()
    {      
        StartCoroutine(DelayTurnOffPanel(instructionPanel));
    }

    IEnumerator DelayTurnOffPanel(GameObject panel)
    {
        yield return new WaitForSeconds(2f);
        instructionPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
    }

    IEnumerator PauseWithDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Time.timeScale = 0;
    }
}
