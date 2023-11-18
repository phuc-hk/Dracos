using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private static TaskManager instance;

    public static TaskManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TaskManager>();
            }
            return instance;
        }
    }

    public List<Task> tasks;
    void Start()
    {
        foreach (Task task in tasks)
        {
            task.OnComplete.AddListener(TaskComplete);
        }
    }

    private void TaskComplete()
    {
        UIManager.Instance.ShowWinPanel();
    }
}
