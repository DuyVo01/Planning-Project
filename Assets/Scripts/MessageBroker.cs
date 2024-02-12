using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBroker : MonoBehaviour
{
    public static MessageBroker Instance { get; private set; }

    private Dictionary<string, List<Action<object>>> subscribers = new Dictionary<string, List<Action<object>>>();

    private struct Message
    {
        public string eventName;
        public object eventData;

        public Message(string eventName, object eventData)
        {
            this.eventName = eventName;
            this.eventData = eventData;
        }
    }

    private Queue<Message> messageQueue = new Queue<Message>();
    private bool isProcessingMessages = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void Subscribe(string eventName, Action<object> listener)
    {
        if (!subscribers.ContainsKey(eventName))
        {
            subscribers[eventName] = new List<Action<object>>();
        }

        subscribers[eventName].Add(listener);
    }

    public void UnSubscribe(string eventName, Action<object> listener)
    {
        if (subscribers.ContainsKey(eventName))
        {
            subscribers[eventName].Remove(listener);
        }
    }

    public void Publish(string eventName, object eventData)
    {
        messageQueue.Enqueue(new Message(eventName, eventData));

        if (!isProcessingMessages)
        {
            StartCoroutine(ProcessMessages());
        }
    }

    private IEnumerator ProcessMessages()
    {
        isProcessingMessages = true;

        while(messageQueue.Count > 0)
        {
            Message message = messageQueue.Dequeue();

            if (subscribers.ContainsKey(message.eventName))
            {
                foreach (var listener in subscribers[message.eventName])
                {
                    listener.Invoke(message.eventData);
                    yield return null;
                }
            }
        }

        isProcessingMessages = false;
    }
}
