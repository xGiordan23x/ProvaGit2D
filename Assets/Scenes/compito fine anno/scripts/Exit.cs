using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour, ISubscriber
{
    public string messageName = "Exit";
    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(messageName, this);
    }
    public void OnNotify(object content)
    {
        PubSub.Instance.SendMessage("GameManager", this);
    }
}
