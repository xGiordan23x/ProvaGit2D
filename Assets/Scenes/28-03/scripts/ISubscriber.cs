using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubscriber
{
   
    public void OnNotify(object content);
}
