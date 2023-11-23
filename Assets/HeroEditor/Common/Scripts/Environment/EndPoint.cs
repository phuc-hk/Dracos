using Assets.HeroEditor.Common.CharacterScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject barrie;
    public ParticleSystem firework;
    public ParticleSystem confetti;
    public AudioClip victorySound;

    private AudioSource audioSource;

    private void OnEnable()
    {
        TaskManager.Instance.OnTaskComplete.AddListener(ActiveEndPoint);
    }

    //private void OnDisable()
    //{
    //    TaskManager.Instance.OnTaskComplete.RemoveListener(ActiveEndPoint);
    //}

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void ActiveEndPoint()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (barrie != null)
                barrie.SetActive(false);
            //other.GetComponentInChildren<Animator>().SetBool("Victory", true);
            StartCoroutine(PlayMusic());
            StartCoroutine(Firework());
            StartCoroutine(PlayerCelerate(other.gameObject));
            StartCoroutine(Win());
        }
    }
    IEnumerator Firework()
    {
        yield return new WaitForSeconds(3f);
        firework.Play();
        confetti.Play();
    }
    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(victorySound);
    }

    IEnumerator PlayerCelerate(GameObject player)
    {
        yield return new WaitForSeconds(1f);
        player.GetComponentInChildren<Animator>().SetBool("Victory", true);
        GameObject princess = GameObject.Find("Princess");
        if (princess != null)
            princess.GetComponent<Character>().SetExpression("Happy");
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(8f);
        UIManager.Instance.ShowWinPanel();
    }
}
