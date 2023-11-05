using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSensor : MonoBehaviour
{
    public Transform playerOriginalTransform;
    private Vector3 originalTranform;

    void Start()
    {
        originalTranform = playerOriginalTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game over");
            other.GetComponent<Movement>().enabled = false;
            other.gameObject.transform.position = originalTranform;
            StartCoroutine(FlashPlayer(other.gameObject));
            StartCoroutine(EnableMovement(other.gameObject.GetComponent<Movement>()));
        }
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

    private IEnumerator EnableMovement(Movement movement)
    {
        yield return new WaitForSeconds(0.5f);
        movement.enabled = true;
    }
}
