using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBossAttacking : MonoBehaviour
{
    public float attackRange = 4f;
    public AnimationEvents AnimationEvents;
    public Transform Edge;
    public GameObject bulletPrefab;
    public Transform firePos;
    //private int damage = 1;
    private Character target;

    /// <summary>
    /// Listen animation events to determine hit moments.
    /// </summary>
    public void Start()
    {
        AnimationEvents.OnCustomEvent += OnAnimationEvent;
    }

    public void OnDestroy()
    {
        AnimationEvents.OnCustomEvent -= OnAnimationEvent;
    }

    private void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "Attack":
                Attack(target);
                break;
            default: return;
        }
    }

    public void Attack(Character player)
    {
        if (player == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        GameObject bulletObject = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
        EnemyBullet bullet = bulletObject.GetComponent<EnemyBullet>();
        bullet.Fire(player.transform.position);
    }

    public void AssignTarget(Character targetToAssign)
    {
        if (target == null)
            target = targetToAssign;
    }
}
