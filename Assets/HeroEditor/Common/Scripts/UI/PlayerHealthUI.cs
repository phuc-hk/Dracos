using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider healthSlider;

    private void Start()
    {
        playerHealth.OnHealthChange.AddListener(UpdateSliderValue);
        healthSlider.value = 1;
    }

    private void UpdateSliderValue()
    {
        healthSlider.value = playerHealth.GetHealthPercentage();
    }
}
