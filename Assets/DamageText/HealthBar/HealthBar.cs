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
        float scaleValue = health.GetHealthPercentage();
        foreground.localScale = new Vector3(scaleValue, 1, 1);
        if (health.IsDie())
        {
            StartCoroutine(TurnOffHealthBar());
            
        }
    }

    IEnumerator TurnOffHealthBar()
    {
        yield return new WaitForSeconds(0.5f);
        rootCanvas.enabled = false;
    }

    
}
