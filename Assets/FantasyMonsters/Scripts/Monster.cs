﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.FantasyMonsters.Scripts.Tweens;
using UnityEngine;

namespace Assets.FantasyMonsters.Scripts
{
    /// <summary>
    /// The main script to control monsters.
    /// </summary>
    public class Monster : MonoBehaviour
    {
        public SpriteRenderer Head;
        public List<Sprite> HeadSprites;
        public Animator Animator;
        public bool Variations;
        public event Action<string> OnEvent = eventName => { };

        //New add
        public GameObject detectRange;
        public Transform player;
        private EnemyMovement enemyMovement;
        private EnemyAttacking enemyAttacking;
        float distanceToPlayer;


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

            GetComponent<LayerManager>().SetSortingGroupOrder((int) -transform.localPosition.y);

            var stateHandler = Animator.GetBehaviours<StateHandler>().SingleOrDefault(i => i.Name == "Death");

            if (stateHandler)
            {
                stateHandler.StateExit.AddListener(() => SetHead(0));
            }

            //New add
            enemyMovement = GetComponent<EnemyMovement>();
            enemyAttacking = GetComponent<EnemyAttacking>();
        }

        private void Start()
        {
            detectRange.SetActive(true);
        }
        private void Update()
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > enemyMovement.detectRange)
            {
                detectRange.SetActive(true);
                enemyMovement.Patrol();
                SetState(MonsterState.Walk);
            }
            else if (distanceToPlayer > enemyAttacking.attackRange)
            {
                detectRange.SetActive(false);
                enemyMovement.Chase(player);
                SetState(MonsterState.Run);
            }
            else
            {
                enemyMovement.Stop();
                enemyAttacking.Attack();              
                Animator.SetTrigger("Attack");
            }
        }

        /// <summary>
        /// Set animation parameter State to control transitions. Play different state animations (except Attack).
        /// </summary>
        public void SetState(MonsterState state)
        {
            Animator.SetInteger("State", (int) state);
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
            if (index != 2 && Animator.GetInteger("State") == (int) MonsterState.Death) return;

            if (index < HeadSprites.Count)
            {
                Head.sprite = HeadSprites[index];
            }
        }
    }
}