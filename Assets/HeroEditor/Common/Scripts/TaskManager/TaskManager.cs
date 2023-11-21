using Assets.HeroEditor.Common.CharacterScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public bool isTaskComplete = false;

    public UnityEvent OnTaskComplete;
    void Start()
    {
        foreach (Task task in tasks)
        {
            task.OnComplete.AddListener(TaskComplete);
        }
    }

    private void TaskComplete()
    {
        isTaskComplete = true;
        UIManager.Instance.ShowInstructionPanel();
        OnTaskComplete?.Invoke();
        //UIManager.Instance.ShowWinPanel();
    }
}
