﻿using Assets.FantasyMonsters.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using UnityEngine;

namespace Assets.HeroEditor.Common.ExampleScripts
{
    /// <summary>
    /// General melee weapon behaviour.
    /// First thing you need to check is hit event. Use animation events or check user input.
    /// Second thing is to resolve impacts to other objects (enemies, environment). Use collisions or raycasts.
    /// </summary>
    public class MeleeWeapon : MonoBehaviour
    {
        public AnimationEvents AnimationEvents;
        public Transform Edge;
        private int damage = 2;
        private int hitCount = 0;
        [SerializeField] GameObject slashEffect;
        [SerializeField] GameObject axeWeapon;
        [SerializeField] Transform axePos;

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
                case "Hit":
                    // Place hit behaviour here. For example, you could check/raycast collisons here.
                    Collider2D[] hitColliders = Physics2D.OverlapBoxAll(Edge.position, Edge.localScale, 0);
                    foreach (Collider2D hitCollider in hitColliders)
                    {
                        CombatTarget combatTarget = hitCollider.GetComponent<CombatTarget>();
                        if (combatTarget == null) continue;
                        combatTarget.GetComponent<Health>().TakeDamage(damage);
                        hitCount++;
                        if (hitCount >= 2)
                        {
                            if (isWeapon())
                                slashEffect.SetActive(true);
                            //combatTarget.GetComponent<Monster>().IsFlyBack(true);
                            hitCount = 0;
                        }
                    }
                    break;
                case "Throw":
                    //Debug.Log("Throw axe ne");
                    Instantiate(axeWeapon, axePos.position, Quaternion.identity);
                    break;
                default: return;
            }
        }

        public void SetWeaponDamage(int damage)
        {
            this.damage = damage;
        }

        private bool isWeapon()
        {
            return GetComponent<SpriteRenderer>().sprite != null;
        }
    }
}