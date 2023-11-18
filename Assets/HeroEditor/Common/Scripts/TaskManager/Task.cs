using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    public string _name;
    public bool isComplete;
    public UnityEvent OnComplete;
    public Task(string name)
    {
        this._name = name;
        this.isComplete = false;
    }
}
