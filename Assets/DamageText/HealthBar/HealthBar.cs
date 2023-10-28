using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Canvas rootCanvas;
    [SerializeField] RectTransform foreground;
    Health health;

    void Awake()
    {
        health = GetComponentInParent<Health>();
    }

    private void Start()
    {
        health.OnHealthChange.AddListener(UpdateHealthBar);
        rootCanvas.enabled = false;
        
    }

    private void UpdateHealthBar()
    {
        rootCanvas.enabled = true;
        if (health.IsDie())
        {
            rootCanvas.enabled = false;
        }
        float scaleValue = health.GetHealthPercentage();
        foreground.localScale = new Vector3(scaleValue, 1, 1);
    }
}
