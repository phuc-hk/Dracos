using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    public void DestroyText()
    {
        Destroy(gameObject);
    }

    public void SetValue(float amount)
    {
        damageText.text = amount.ToString();
    }
}
