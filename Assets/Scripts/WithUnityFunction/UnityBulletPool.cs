using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnityBulletPool : MonoBehaviour
{
    public static UnityBulletPool Instance { get; private set; }
    [SerializeField] private UnityBullet bulletPrefab;

    private ObjectPool<UnityBullet> _pool;
    private void Start()
    {
        Instance = this;
        _pool = new(InstantiateBullet, TakeFromPool, returnToPool);
    }

    private UnityBullet InstantiateBullet()
    {
        return Instantiate(bulletPrefab,this.gameObject.transform);
    }
    private void TakeFromPool(UnityBullet newBullet)
    {
        newBullet.gameObject.SetActive(true);
    }
    private void returnToPool(UnityBullet newBullet)
    {
        newBullet.gameObject.SetActive(false);
    }

    public UnityBullet GetObject()
    {
        return _pool.Get();
    }
    public void ReturnObject(UnityBullet obj)
    {
        _pool.Release(obj);
    }
}

