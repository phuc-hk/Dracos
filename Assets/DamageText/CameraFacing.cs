using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing : MonoBehaviour
{
    private Transform parentTransform;
    private Vector3 childScale;

    void Start()
    {
        parentTransform = transform.parent;
        childScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (parentTransform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-childScale.x, childScale.y, childScale.z);
        }
        else
        {
            transform.localScale = new Vector3(childScale.x, childScale.y, childScale.z);
        }
    }
}

