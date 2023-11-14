using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HealthSupplyPickup : MonoBehaviour
{
    [SerializeField] int recoverAmount;
    public AudioSource audioSource;
    public AudioClip healSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(healSound);
            }
            //Debug.Log("Recoverrrr");
            RecoverHealth(other.GetComponent<PlayerHealth>());
            StartCoroutine(DestroyGameObject());
            //StartCoroutine(HideForSeconds(5));
        }

    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //Debug.Log("Recoverrrr");
    //        RecoverHealth(collision.gameObject.GetComponent<PlayerHealth>());
    //        //Destroy(gameObject);
    //        //StartCoroutine(HideForSeconds(5));
    //    }
    //}

    private void RecoverHealth(PlayerHealth playerHealth)
    {
        playerHealth.Recover(recoverAmount);
    }
}
