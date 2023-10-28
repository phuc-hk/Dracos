using Assets.FantasyMonsters.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    float health = -1;
    bool isDie = false;
    SpriteRenderer[] spriteRenderers;
    public UnityEvent OnHealthChange;
    public TakeDamageEvent OnTakeDamage;
    [Serializable]
    public class TakeDamageEvent : UnityEvent<float>
    {

    }

    void Start()
    {
        if (health < 0)
        {
            health = 5;
        }
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        Debug.Log("Health " + health);
        if (health == 0)
        {
            Die();
        }
        else
        {
            OnTakeDamage?.Invoke(damage);
        }
    }

    private void Die()
    {
        isDie = true;
        GetComponent<Animator>().SetInteger("State", (int)MonsterState.Death);
        StartCoroutine(FlashSprite());
    }

    IEnumerator FlashSprite()
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = false;
            }
            yield return new WaitForSeconds(0.3f);
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = true;
            }
            yield return new WaitForSeconds(0.3f);
        }
        Destroy(gameObject);
    }

    public bool IsDie()
    {
        return isDie;
    }

    public float GetCurrentHealth()
    {
        return health;
    }
}
