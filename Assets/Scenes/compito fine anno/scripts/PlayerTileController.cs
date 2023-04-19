using UnityEditor.Rendering;
using UnityEngine;

public class PlayerTileController : MonoBehaviour, ISubscriber
{
    private string messageName = "Player";
    private bool playerTurn;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private LayerMask doorMask;
    [SerializeField] private Transform KeyPosition;
    private bool hasKey;
    private Key keyHold;

    private bool canMoveRight;
    private bool canMoveLeft;
    private bool canMoveUp;
    private bool canMoveDown;

   
    private void Start()
    {
        messageName = "Player";
        PubSub.Instance.RegisteredSubscriber(messageName, this);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (playerTurn)
        {
            CheckNearTiles(hasKey);
            //left right movement
            if (Input.GetButtonDown("Horizontal"))
            {
                float horizontal = Input.GetAxis("Horizontal");
                if (horizontal > 0 && canMoveRight)
                {
                    transform.position += new Vector3(1, 0, 0);
                    playerTurn = false;
                }
                else if (horizontal < 0 && canMoveLeft)
                {
                    transform.position += new Vector3(-1, 0, 0);
                    playerTurn = false;
                }


            }
            //up down movement
            if (Input.GetButtonDown("Vertical"))
            {
                float vertical = Input.GetAxis("Vertical");
                if (vertical > 0 && canMoveUp)
                {
                    transform.position += new Vector3(0, 1, 0);
                    playerTurn = false;
                }
                else if (vertical < 0 && canMoveDown)
                {
                    transform.position += new Vector3(0, -1, 0);
                    playerTurn = false;
                }

            }

        }
        else
        {
            PubSub.Instance.SendMessage("GameManager", this);
        }
    }

    private void CheckNearTiles(bool hasKey)
    {

        CheckForWallAndDoor();
        if (hasKey)
        {
            CheckForDoor();
        }

    }

    private void CheckForWallAndDoor()
    {

        //sinistra
        if (Physics2D.Raycast(transform.position, Vector2.left, 1, wallMask) == true || Physics2D.Raycast(transform.position, Vector2.left, 1, doorMask) == true)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }
        //destra
        if (Physics2D.Raycast(transform.position, Vector2.right, 1, wallMask) == true || Physics2D.Raycast(transform.position, Vector2.right, 1, doorMask) == true)
        {
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }
        //su
        if (Physics2D.Raycast(transform.position, Vector2.up, 1, wallMask) == true || Physics2D.Raycast(transform.position, Vector2.up, 1, doorMask) == true)
        {
            canMoveUp = false;
        }
        else
        {
            canMoveUp = true;
        }
        //giu
        if (Physics2D.Raycast(transform.position, Vector2.down, 1, wallMask) == true || Physics2D.Raycast(transform.position, Vector2.down, 1, doorMask) == true)
        {
            canMoveDown = false;
        }
        else
        {
            canMoveDown = true;
        }
    }
    private void CheckForWall()
    {

        //sinistra
        if (Physics2D.Raycast(transform.position, Vector2.left, 1, wallMask) == true)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }
        //destra
        if (Physics2D.Raycast(transform.position, Vector2.right, 1, wallMask) == true)
        {
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }
        //su
        if (Physics2D.Raycast(transform.position, Vector2.up, 1, wallMask) == true)
        {
            canMoveUp = false;
        }
        else
        {
            canMoveUp = true;
        }
        //giu
        if (Physics2D.Raycast(transform.position, Vector2.down, 1, wallMask) == true)
        {
            canMoveDown = false;
        }
        else
        {
            canMoveDown = true;
        }
    }
    private void CheckForDoor()
    {
        //left
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.left, 1, doorMask);
        if (hit1 == true)
        {
            Door door = hit1.rigidbody.GetComponent<Door>();
            if (door.color == keyHold.color)
            {
                door.Open();
                hasKey = false;
                Destroy(keyHold.gameObject);
            }
        }
        //right
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right, 1, doorMask);
        if (hit2 == true)
        {
            Door door = hit2.rigidbody.GetComponent<Door>();
            if (door.color == keyHold.color)
            {
                door.Open();
                hasKey = false;
                Destroy(keyHold.gameObject);
            }
        }
        //up
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Vector2.up, 1, doorMask);
        if (hit3 == true)
        {
            Door door = hit3.rigidbody.GetComponent<Door>();
            if (door.color == keyHold.color)
            {
                door.Open();
                hasKey = false;
                Destroy(keyHold.gameObject);
            }
        }
        //down
        RaycastHit2D hit4 = Physics2D.Raycast(transform.position, Vector2.down, 1, doorMask);
        if (hit4 == true)
        {
            Door door = hit4.rigidbody.GetComponent<Door>();
            if (door.color == keyHold.color)
            {
                door.Open();
                hasKey = false;
                Destroy(keyHold.gameObject);
            }
        }

    }

    public void OnNotify(object content)
    {
        if (content is GameManager)
        {
            playerTurn = true;
        }
        if (content is Key)
        {
            hasKey = true;

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Exit exit = collision.GetComponent<Exit>();
        Pickupable pickUp = collision.GetComponent<Pickupable>();
        if(exit!= null)
        {
            PubSub.Instance.SendMessage(exit.messageName, this);
        }
        if (pickUp is Key && hasKey)
        {
            return;
        }
        else if (pickUp is Key && !hasKey)
        {
            pickUp.gameObject.transform.SetParent(transform,false);
            pickUp.gameObject.transform.position = KeyPosition.transform.position;
            keyHold = collision.GetComponent<Key>();
            keyHold.color = collision.GetComponent<Key>().color;
        }
        if (pickUp != null)
        {
            PubSub.Instance.SendMessage(pickUp.messageName, this);
        }
    }
}
