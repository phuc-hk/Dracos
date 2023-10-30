using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public Character character;
    protected override void Die()
    {
        isDie = true;
        character.SetState(CharacterState.DeathB);
        StartCoroutine(FlashSprite());
    }
}
