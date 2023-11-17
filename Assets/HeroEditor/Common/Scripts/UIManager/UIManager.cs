using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject guestPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public Health health;

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
        guestPanel.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        guestPanel.SetActive(false);
        losePanel.SetActive(true);
        winPanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
