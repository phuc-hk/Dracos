using Assets.FantasyMonsters.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    float heath = -1;

    void Start()
    {
        if (heath < 0)
        {
            heath = 5;
        }
    }
    
    public void TakeDamage(float damage)
    {
        heath = Mathf.Max(heath - damage, 0);
        Debug.Log("Health " + heath);
        if (heath == 0)
        {
            Die();
        }
    }


    private void Die()
    {
        GetComponent<Animator>().SetInteger("State", (int) MonsterState.Death); ;
        GetComponent<Collider2D>().enabled = false;
    }

    public bool IsDie()
    {
        return heath == 0;
    }


    public float GetCurrentHealth()
    {
        return heath;
    }

   
}
