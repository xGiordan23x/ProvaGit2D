using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    public Transform shootingPoint;
    [SerializeField] private float shootingSpeed;
    private float shootingTime = 0;


    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(horizontal, 0, 0);

        if (Input.GetMouseButton(1))
        {
            Shoot(true);
        }
        else if (Input.GetMouseButton(0))
        {
            Shoot(false);
        }
    }


    public void Shoot(bool right)
    {
        shootingTime -= Time.deltaTime;

        if (shootingTime < 0)
        {
            UnityBullet newBullet = UnityBulletPool.Instance.GetObject();
            newBullet.transform.position = shootingPoint.transform.position;
            if (right)

                newBullet.rb.AddForce(transform.right * newBullet.force);

            else if(!right)
                newBullet.rb.AddForce(-transform.right * newBullet.force);



            shootingTime = shootingSpeed;
        }

    }
}

