using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityHeart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            UnityHeartPool.Instance.ReturnObject(this);

        }
    }
}
