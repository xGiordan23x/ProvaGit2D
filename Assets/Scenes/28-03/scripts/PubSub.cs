using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubSub : MonoBehaviour
{

    private static PubSub _instance;
    public static PubSub Instance
    {
        get
        {
            if (_instance != null) return _instance;
            GameObject pubsubOIbject = new GameObject("# PubSub");
            _instance = pubsubOIbject.AddComponent<PubSub>();
            return _instance;
        }
    }




    private Dictionary<string, List<ISubscriber>> _subscribers = new();

    public void RegisteredSubscriber(string messageType, ISubscriber subscriber)
    {
        if (_subscribers.ContainsKey(messageType))
        {
            _subscribers[messageType].Add(subscriber);
           
        }
        else
        {
            List<ISubscriber> newList = new();
            newList.Add(subscriber);
            _subscribers.Add(messageType, newList);
        }

       


    }


    public void SendMessage(string messageType, object content)
    {
        if (!_subscribers.ContainsKey(messageType)) return;

        
        foreach (ISubscriber subscriber in _subscribers[messageType])
        {
            subscriber.OnNotify(content);
        }
    }
}
