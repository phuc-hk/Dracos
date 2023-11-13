using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip defaultFootstepSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.CompareTag("Ground"))
    //    {
    //        if (!audioSource.isPlaying)
    //            audioSource.PlayOneShot(defaultFootstepSound);
    //    }
    //}
}
