using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickupable
{
    public int id;
    public Color color;
    private void Start()
    {
        GameManager.Instance.GetKeyMessageName(this);
        PubSub.Instance.RegisteredSubscriber(messageName, this);
        GetComponent<SpriteRenderer>().color= color;
    }
    public override void ItemGrabbed()
    {      
        PubSub.Instance.SendMessage("GameManager", this);
       
    }

    public override void OnNotify(object content)
    {
        ItemGrabbed();
    }
}
