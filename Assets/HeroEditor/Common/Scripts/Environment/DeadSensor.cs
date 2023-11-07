using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSensor : MonoBehaviour
{
    [SerializeField] float damage;
    private bool isDie;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EnableMovement(other.gameObject.GetComponent<Movement>()));
            StartCoroutine(ChangeExpression(other.gameObject.GetComponent<Character>()));
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            isDie = other.GetComponent<PlayerHealth>().IsDie();
            StartCoroutine(FlashPlayer(other.gameObject));
            StartCoroutine(MoveToCheckPoint(other.gameObject));
        }
    }

    private IEnumerator MoveToCheckPoint(GameObject player)
    {       
        yield return new WaitForSeconds(1f);
        if (!isDie)
            player.transform.position = player.GetComponent<Character>().lastCheckpoint.position;
    }

    private IEnumerator FlashPlayer(GameObject player)
    {
        SpriteRenderer[] spriteRenderers = player.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = false;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = true;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator ChangeExpression(Character character)
    {
        character.SetExpression("Dead");
        yield return new WaitForSeconds(1.2f);
        if (!isDie)
            character.SetExpression("Default");
    }

    private IEnumerator EnableMovement(Movement movement)
    {
        movement.enabled = false;
        yield return new WaitForSeconds(1.2f);
        if (!isDie)
            movement.enabled = true;
    }
}
