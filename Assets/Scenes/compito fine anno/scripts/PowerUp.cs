using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Pickupable
{
    public int id;
    
    private void Start()
    {
      GameManager.Instance.GetPowerUpMessageName(this);      
      PubSub.Instance.RegisteredSubscriber(messageName, this);
    }
    public override void ItemGrabbed()
    {
        
        PubSub.Instance.SendMessage("GameManager", this);
        Destroy(gameObject);


    }

    public override void OnNotify(object content)
    {
        ItemGrabbed();
    }
}
