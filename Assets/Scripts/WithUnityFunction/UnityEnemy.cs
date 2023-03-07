using UnityEngine;

public class UnityEnemy : MonoBehaviour
{
   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<UnityBullet>())
        {
            UnityHeart newHeart = UnityHeartPool.Instance.GetObject();
            newHeart.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
