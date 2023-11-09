using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("ting ting");
            ScoreModel.instance.IncrementScore(coinValue);
            Destroy(gameObject);
        }
    }
}
