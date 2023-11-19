using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    [SerializeField] float initialHealth;
    [SerializeField] public float health;
    protected bool isDie = false;
    protected SpriteRenderer[] spriteRenderers;
    public UnityEvent OnHealthChange;
    public TakeDamageEvent OnTakeDamage;
    public UnityEvent OnDie;

    [Serializable]
    public class TakeDamageEvent : UnityEvent<float>
    {

    }

    protected virtual void Start()
    { 
        health = initialHealth;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDie) return;
        health = Mathf.Max(health - damage, 0);
        if (health == 0)
        {
            Die();
            OnDie?.Invoke();
        }
        else
        {
            OnTakeDamage?.Invoke(damage);
        }
        OnHealthChange?.Invoke();
    } 

    protected abstract void Die();

    protected IEnumerator FlashSprite()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 2; i++)
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
        Destroy(gameObject, 0.5f);
    }

    public bool IsDie()
    {
        return isDie;
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    internal float GetHealthPercentage()
    {
        return health / initialHealth;
    }

}
