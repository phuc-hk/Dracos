using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatWildBoarTask : Task
{
    public int wildBoarCount;
    public int wildBoarNeedToDefeat = 1;

    void Start()
    {
        //EnemyHealth[] wildBoars = FindObjectsOfType<EnemyHealth>();
        //foreach (EnemyHealth wildBoar in wildBoars)
        //{
        //    wildBoar.OnDie.AddListener(IncrementWildBoarCount);
        //}
        EnemyHealth bossBoarHealth = FindObjectOfType<EnemyBoss>().GetComponent<EnemyHealth>();
        bossBoarHealth.OnDie.AddListener(IncrementWildBoarCount);
    }

    private void IncrementWildBoarCount()
    {       
        wildBoarCount++;
        if (wildBoarCount >= wildBoarNeedToDefeat)
        {
            isComplete = true;
            OnComplete?.Invoke();
        }
    }
    public DefeatWildBoarTask(string name) : base(name)
    {
        this.wildBoarCount = 0;
    }
}
