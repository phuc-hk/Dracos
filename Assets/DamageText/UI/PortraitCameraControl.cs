using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitCameraControl : MonoBehaviour
{
    public Transform playerHead;
    public Vector3 offset;
    void LateUpdate()
    {
        if (playerHead != null)
            transform.position = playerHead.position + offset;
    }
}
