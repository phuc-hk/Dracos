using Assets.FantasyMonsters.Scripts;
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
        private int damage = 1;
        private int hitCount = 0;

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
                        StartCoroutine(FlyBack(combatTarget));
                    }
                    break;
                default: return;
            }
        }

        IEnumerator FlyBack(CombatTarget combatTarget)
        {
            hitCount++;
            if (hitCount >= 2)
            {
                EnemyMovement movement = combatTarget.GetComponent<EnemyMovement>();
                if (movement != null)
                {
                    Debug.Log("Fly back");
                    combatTarget.GetComponent<Monster>().enabled = false;
                    movement.FlyBack();
                    yield return new WaitForSeconds(1);
                    combatTarget.GetComponent<Monster>().enabled = true;
                }
                hitCount = 0;
            }
        }
    }
}