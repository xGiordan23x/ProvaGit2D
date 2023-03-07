using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnityHeartPool : MonoBehaviour
{
    public static UnityHeartPool Instance { get; private set; }
    [SerializeField] private UnityHeart heartPrefab;

    private ObjectPool<UnityHeart> _pool;
    private void Start()
    {
        Instance= this;
        _pool = new(InstantiateHeart, TakeFromPool, returnToPool);
    }

    private UnityHeart InstantiateHeart()
    {
        return Instantiate(heartPrefab,this.gameObject.transform);
    }
    private void TakeFromPool(UnityHeart newHeart)
    {
        newHeart.gameObject.SetActive(true);
    }
    private void returnToPool(UnityHeart newHeart)
    {
        newHeart.gameObject.SetActive(false);
    }

    public UnityHeart GetObject()
    {
        return _pool.Get();
    }
    public void ReturnObject(UnityHeart obj)
    {
        _pool.Release(obj);
    }
}
