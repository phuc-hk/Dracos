using RPGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Canvas rootCanvas;
    [SerializeField] RectTransform foreground;
    Heath heath;
    
    // Start is called before the first frame update
    void Awake()
    {
        heath = GetComponentInParent<Heath>();
    }

    private void Start()
    {
        heath.OnHealthChange.AddListener(UpdateHealthBar);
        rootCanvas.enabled = false;
    }

    private void UpdateHealthBar()
    {
        rootCanvas.enabled = true;
        if (heath.IsDie())
        {
            rootCanvas.enabled = false;
        }
        //float scaleValue = heath.GetHealthPercentage() / 100;
        //foreground.localScale = new Vector3(scaleValue, 1, 1);
    }
}
