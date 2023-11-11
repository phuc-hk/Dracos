using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public delegate void OnEnemyDestroyed(Vector3 deathPosition, GameObject supplyItem);
    public static event OnEnemyDestroyed EnemyDestroyed;
    public GameObject supplyItem;
    protected override void Die()
    {
        isDie = true;       
        GetComponent<Animator>().SetInteger("State", (int)MonsterState.Death);
        StartCoroutine(FlashSprite());
        EnemyDestroyed(transform.position, supplyItem);
    }
}
