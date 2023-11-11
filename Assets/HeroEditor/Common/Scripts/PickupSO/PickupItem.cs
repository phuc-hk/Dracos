using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Item", menuName = "Supply Pickup Item", order = 1)]
public class SupplyPickupItem : ScriptableObject
{
    public GameObject supplyPickupPrefab;
    public int amount;

    public void TakeEffect(Character character)
    {
        
    }
}