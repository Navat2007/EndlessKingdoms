using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageQueueManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.MessageQueueManager = this;
    }

    public void Init()
    {
        
    }
    
    public void AddToQueue(MessagesType messageType, string title, string message, Action callback = null)
    {
        Debug.Log($"New queue message: {messageType} - {title} {message}");
    }
}

public enum MessagesType
{
    ACHIEVEMENT,
    LEVEL
}
