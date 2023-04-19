using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickupable : MonoBehaviour, ISubscriber
{
    public string messageName;

    public virtual void OnNotify(object content)
    {
        
    }
    public virtual void ItemGrabbed()
    {

    }
}
