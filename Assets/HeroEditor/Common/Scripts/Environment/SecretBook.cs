using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretBook : MonoBehaviour
{
    public GameObject skillButton;
    public ParticleSystem particle;
    public Button button; // The button to be flashed
    public float flashDuration = 1f; // The duration of the flash animation
    public float flashSpeed = 10f; // The speed of the flash animation
    public GameObject panel;
    public AudioSource audioSource;
    public AudioClip rewardSound;

    private Color originalColor; // The original color of the button

    void Start()
    {
        originalColor = button.colors.normalColor; // Save the original color of the button
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(rewardSound);
            //Debug.Log("Unlock new skill");
            skillButton.gameObject.SetActive(true);
            StartCoroutine(PlayFireWork());
            StartCoroutine(DisplayPanel());
            StartCoroutine(FlashButton()); // Start the flash animation
        }
    }

    IEnumerator PlayFireWork()
    {
        particle.Play();     
        yield return new WaitForSeconds(2);
        particle.Stop();
        
    }

    IEnumerator DisplayPanel()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panel.SetActive(false);
    }

    IEnumerator FlashButton()
    {
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            float t = Mathf.PingPong(elapsedTime * flashSpeed, 1f);
            ColorBlock colors = button.colors;
            colors.normalColor = Color.Lerp(originalColor, Color.blue, t);
            button.colors = colors;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ColorBlock resetColors = button.colors;
        resetColors.normalColor = originalColor;
        button.colors = resetColors; // Reset the color of the button
    }
}
