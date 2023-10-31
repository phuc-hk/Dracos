using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    public float attackRange = 4f;
    public AnimationEvents AnimationEvents;
    public Transform Edge;
    private int damage = 1;

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
                // Place hit behaviour here. For example, you could check/raycast collisons here.
                Collider[] hitColliders = Physics.OverlapSphere(Edge.position, 1);
                foreach (Collider hitCollider in hitColliders)
                {
                    PlayerHealth health = hitCollider.GetComponent<PlayerHealth>();
                    if (health == null) continue;
                    health.TakeDamage(damage);
                }
                break;
            default: return;
        }
    }
}
