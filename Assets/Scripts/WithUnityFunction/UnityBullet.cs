using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityBullet : MonoBehaviour
{
    public int force;
    public int lifeTime;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        Invoke(nameof(DismissBullet), lifeTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
            DismissBullet();
        
    }
    public void DismissBullet()
    {
        UnityBulletPool.Instance.ReturnObject(this);
    }
}
