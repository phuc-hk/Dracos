using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public ParticleSystem exploseFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("ting ting");
            ScoreModel.instance.IncrementScore(coinValue);
            StartCoroutine(CoinExplose());
        }
    }

    IEnumerator CoinExplose()
    {
        exploseFX.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
