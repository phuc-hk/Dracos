using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTrollBossTask : Task
{
    public DefeatTrollBossTask(string name) : base(name)
    {
    }
    void Start()
    {    
        EnemyHealth trollBoss = GameObject.Find("Troll").GetComponent<EnemyHealth>();
        trollBoss.OnDie.AddListener(DefeatTrollBoss);
    }

    private void DefeatTrollBoss()
    { 
        OnComplete?.Invoke();  
    }
}
