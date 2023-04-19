using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Pickupable
{
    public int id;
    public int scoreToGive;
    private void Start()
    {
        GameManager.Instance.GetTreasureMessageName(this);
        PubSub.Instance.RegisteredSubscriber(messageName, this);
    }
    public override void ItemGrabbed()
    {
        
        PubSub.Instance.SendMessage("GameManager",this);
        GameManager.Instance.IncreaseScore(scoreToGive);
        Destroy(gameObject);

    }

    public override void OnNotify(object content)
    {
        ItemGrabbed();
    }
}
