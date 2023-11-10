using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public delegate void OnEnemyDestroyed(Vector3 deathPosition);
    public static event OnEnemyDestroyed EnemyDestroyed;
    public string itemName;
    protected override void Die()
    {
        isDie = true;       
        GetComponent<Animator>().SetInteger("State", (int)MonsterState.Death);
        StartCoroutine(FlashSprite());
        EnemyDestroyed(transform.position);
    }
}
