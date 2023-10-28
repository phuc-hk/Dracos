using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace RPGame.Combat 
{
    public class Heath : MonoBehaviour
    {
        float heath = -1;
        public UnityEvent OnDeath;
        public UnityEvent OnHealthChange;
        //public UnityEvent<float> OnTakeDamage;
        public TakeDamageEvent OnTakeDamage;
        [Serializable]
        public class TakeDamageEvent: UnityEvent<float>
        {

        }

        void Start()
        {
            if (heath < 0)
            {
                //heath = GetComponent<BaseStats>().GetStat(Stat.Health);
            }               
        }

        void Update()
        {
            OnHealthChange?.Invoke();
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            heath = Mathf.Max(heath - damage, 0);
            OnHealthChange?.Invoke();
            if (heath == 0)
            {
                GainExperience(instigator);
                Die();
            }
            else
            {
                OnTakeDamage?.Invoke(damage);
            }
        }

        private void GainExperience(GameObject instigator)
        {
            //Experience experience = instigator.GetComponent<Experience>();
            //BaseStats baseStats = GetComponent<BaseStats>();
            //experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("death");
            GetComponent<BoxCollider>().enabled = false;
            OnDeath?.Invoke();
        }

        public bool IsDie()
        {
            return heath == 0;
        }

        public object CaptureState()
        {
            return heath;
        }

        public void RestoreState(object state)
        {
            heath = (float)state;

            if (heath == 0)
            {
                GetComponent<Animator>().SetTrigger("death");
                GetComponent<BoxCollider>().enabled = false;
                OnDeath?.Invoke();
            }

        }

        public float GetCurrentHealth()
        {
            return heath;
        }

     
    }
}


