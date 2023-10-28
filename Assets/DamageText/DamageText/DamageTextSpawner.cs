using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] DamageText damageTextPrefab = null;
    //void Start()
    //{
    //    Spawn(10);
    //}

    public void Spawn(float damageAmount)
    {
       DamageText instance =  Instantiate<DamageText>(damageTextPrefab, transform);
       instance.SetValue(damageAmount);
    }
}