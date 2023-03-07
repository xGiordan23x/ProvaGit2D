using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPool : MonoBehaviour
{
    public static HeartPool Instance { get; private set; }

    [SerializeField] private Heart heartPrefab;
    [SerializeField] private int poolSize;

    private Stack<Heart> _pool = new();

    private void Start()
    {
        Instance = this;
        for(int i=0;i < poolSize; i++)
        {
            Heart newHeart = Instantiate(heartPrefab, transform.position, Quaternion.identity);
            newHeart.name = "Heart #" + i;
            newHeart.gameObject.SetActive(false);
            _pool.Push(newHeart);
                       
        }
    }

    //GetObject() => prende il primo cuore inutilizzzato dal pool
    public Heart GetObject()
    {
        Heart heart = _pool.Pop();
        heart.gameObject.SetActive(true);
        return heart;
    }
    
    //ReturnObject() => rimette un cuore nel pool
    public void ReturnObject(Heart heart)
    {
       _pool.Push(heart);
        heart.gameObject.SetActive(false);
    }
}
