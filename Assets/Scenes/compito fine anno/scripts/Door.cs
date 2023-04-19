using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Color color;
    public Key key;

    private void Start()
    {
        color = key.color;
        GetComponent<SpriteRenderer>().color = color;
    }
    internal void Open()
    {
        Debug.Log("porta aperta");
        Destroy(gameObject);
    }
}
