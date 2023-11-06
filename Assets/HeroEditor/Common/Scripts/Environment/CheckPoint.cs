using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collide with something");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collide with player");
            other.gameObject.GetComponent<Character>().lastCheckpoint = checkpoint;
        }
    }
}
