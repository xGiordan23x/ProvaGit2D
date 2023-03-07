using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        Heart newHeart = HeartPool.Instance.GetObject();
        newHeart.transform.position = transform.position;
        Destroy(gameObject);
    }
}
