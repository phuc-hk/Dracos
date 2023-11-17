using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.FantasyMonsters.Scripts.Tweens;
using Assets.HeroEditor.Common.CharacterScripts;

namespace Assets.FantasyMonsters.Scripts
{
    public class EnemyBoss : MonoBehaviour
    {
        public SpriteRenderer Head;
        public List<Sprite> HeadSprites;
        public Animator Animator;
        public bool Variations;
        public event Action<string> OnEvent = eventName => { };

        //New add
        public Transform edge;
        public GameObject detectRange;
        public Character target;
        private EnemyMovement enemyMovement;
        private EnemyBossAttacking enemyAttacking;
        private Health enemyHealth;
        private bool isFlyBack = false;

        //public int insideRange;

        /// <summary>
        /// Called on Awake.
        /// </summary>
        public void Awake()
        {
            if (Variations)
            {
                var variations = GetComponents<MonsterVariation>();
                var random = UnityEngine.Random.Range(0, variations.Length + 1);

                if (random > 0)
                {
                    variations[random - 1].Apply();
                }
            }

            GetComponent<LayerManager>().SetSortingGroupOrder((int)-transform.localPosition.y);

            var stateHandler = Animator.GetBehaviours<StateHandler>().SingleOrDefault(i => i.Name == "Death");

            if (stateHandler)
            {
                stateHandler.StateExit.AddListener(() => SetHead(0));
            }

            //New add
            enemyMovement = GetComponent<EnemyMovement>();
            enemyAttacking = GetComponent<EnemyBossAttacking>();
            enemyHealth = GetComponent<Health>();
        }

        private void Start()
        {
            //detectRange.SetActive(true);
        }
        private void Update()
        {
            if (enemyHealth.IsDie()) return;

            target = DetectCharactersInRange(enemyMovement.detectRange);

            if (target == null)
            {
                SetState(MonsterState.Idle);
                //detectRange.SetActive(true);
                enemyMovement.Patrol();
                SetState(MonsterState.Walk);
            }
            else if (target.GetComponent<Health>().IsDie()) return;
            else if (Vector3.Distance(edge.position, target.transform.position) <= enemyMovement.detectRange
                  && Vector3.Distance(edge.position, target.transform.position) > enemyAttacking.attackRange)
            {
                detectRange.SetActive(false);
                enemyMovement.Chase(target.gameObject.transform);
                enemyMovement.LookTo(target.gameObject.transform);
                SetState(MonsterState.Run);
            }
            else if (Vector3.Distance(edge.position, target.transform.position) <= enemyAttacking.attackRange)
            {
                enemyMovement.LookTo(target.gameObject.transform);
                enemyMovement.Stop();
                enemyAttacking.AssignTarget(target);
                Animator.SetTrigger("Attack");
            }
            if (isFlyBack)
            {
                enemyMovement.FlyBack();
                isFlyBack = false;
            }
        }

        public Character DetectCharactersInRange(float detectRadius)
        {
            Collider[] colliders = Physics.OverlapSphere(edge.position, detectRadius);

            foreach (Collider collider in colliders)
            {
                Character character = collider.GetComponent<Character>();

                if (character != null)
                {
                    return character;
                }
            }
            return null;
        }

        /// <summary>
        /// Set animation parameter State to control transitions. Play different state animations (except Attack).
        /// </summary>
        public void SetState(MonsterState state)
        {
            Animator.SetInteger("State", (int)state);
        }

        /// <summary>
        /// Play Attack animation.
        /// </summary>
        public void Attack()
        {
            Animator.SetTrigger("Attack");
        }

        /// <summary>
        /// Play scale spring animation.
        /// </summary>
        public virtual void Spring()
        {
            ScaleSpring.Begin(this, 1f, 1.1f, 40, 2);
        }

        // Play Die animation.
        public void Die()
        {
            SetState(MonsterState.Death);
        }

        /// <summary>
        /// Called from animation. Can be used by the game to handle animation events.
        /// </summary>
        public void Event(string eventName)
        {
            OnEvent(eventName);
        }

        /// <summary>
        /// Called from animation.
        /// </summary>
        public void SetHead(int index)
        {
            if (index != 2 && Animator.GetInteger("State") == (int)MonsterState.Death) return;

            if (index < HeadSprites.Count)
            {
                Head.sprite = HeadSprites[index];
            }
        }

        public void IsFlyBack(bool value)
        {
            isFlyBack = value;
        }
    }
}

