using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public Character character;
    [SerializeField] ParticleSystem healthRecoverFX;
    public AudioClip gameOverSound;
    protected override void Die()
    {
        GetComponent<AudioSource>().PlayOneShot(gameOverSound);
        isDie = true;
        character.gameObject.GetComponent<CharacterController>().enabled = false;
        character.SetState(CharacterState.DeathB);
        StartCoroutine(FlashSprite());
    }

    public void Recover(int recoverAmount)
    {
        healthRecoverFX.Play();
        StartCoroutine(RecoverHealth(recoverAmount));
    }

    protected IEnumerator RecoverHealth(int recoverAmount)
    {
        float recoverPerSecond = recoverAmount / 3;
        for (int i = 0; i < 3; i++)
        {
            health += recoverPerSecond;
            OnHealthChange?.Invoke();
            yield return new WaitForSeconds(1);
        }
    }
}
