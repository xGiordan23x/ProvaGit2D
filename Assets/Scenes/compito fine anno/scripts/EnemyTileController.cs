using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTileController : MonoBehaviour, ISubscriber
{
    private bool stunned;
   [SerializeField] private int turnStunned;
    public int movementToDo;
    public TextMeshProUGUI stunTurnText;
    private int leftTurns;
    public GameObject stunEffect;
    private bool enemyTurn;
    private string messageName = "Enemy";
    public LayerMask wallMask;
    public LayerMask playerMask;
    public LayerMask doorMask;

    private bool canMoveRight;
    private bool canMoveLeft;
    private bool canMoveUp;
    private bool canMoveDown;
    private bool playerFinded = false;

    private void Start()
    {
        stunned = false;
        playerFinded = false;
        messageName = "Enemy";
        PubSub.Instance.RegisteredSubscriber(messageName, this);
    }
    public void OnNotify(object content)
    {
        if(content is GameManager)
        {
            enemyTurn = true;
            if(stunned)
            {
                leftTurns--;
                RefreshStunText();
                if(leftTurns == 0)
                {
                    stunned= false;
                    SetUpStunEffect(stunned);
                    ;
                       
                }
            }
        }
        if(content is PowerUp)
        {
            stunned = true;           
            leftTurns = turnStunned;
            SetUpStunEffect(stunned);
            

        }
        

    }

    private void SetUpStunEffect(bool stunned)
    {
        stunTurnText.gameObject.SetActive(stunned);
        RefreshStunText();
        stunEffect.SetActive(stunned);
    }
    private void RefreshStunText()
    {
        stunTurnText.text = leftTurns.ToString();
    }

    private void Update()
    {

        if (playerFinded)
        {
            Debug.Log("Hai perso");
            
            SceneManager.LoadScene(0);
            
        }
        if (enemyTurn)
        {                                 
                CheckNearTiles();

            if(stunned == false)
            {
               
                Move();
            }
             
            PubSub.Instance.SendMessage("GameManager", this);
            enemyTurn = false;


        }

    }

    public void Move()
    {
        int temp = Random.Range(0, 4);
        switch (temp)
        {
            case (0):
                //destra
                if (canMoveRight)
                {
                    transform.position += new Vector3(1, 0, 0);
                    
                }
                break;


            case (1):
                //sinistra
                if (canMoveLeft)
                {
                    transform.position += new Vector3(-1, 0, 0);
                    
                }
                break;


            case (2):
                //giu
                if (canMoveDown)
                {
                    transform.position += new Vector3(0, -1, 0);
                }
                break;


            case (3):
                //su
                if (canMoveUp)
                {
                    transform.position += new Vector3(0, 1, 0);
                }
                break;

        }
    }
    private void CheckNearTiles()
    {
        //faccio 4 raycast in 4 direzioni

        //destra
        if (Physics2D.Raycast(transform.position, Vector2.right, 1, wallMask) == true || Physics2D.Raycast(transform.position, Vector2.right, 1, doorMask) == true)
        {
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }

        //sinistra
        if (Physics2D.Raycast(transform.position, Vector2.left, 1, wallMask) == true || Physics2D.Raycast(transform.position, Vector2.left, 1, doorMask) == true)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
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

        //su
        if (Physics2D.Raycast(transform.position, Vector2.up, 1, wallMask) == true || Physics2D.Raycast(transform.position, Vector2.up, 1, doorMask) == true)
        {
            canMoveUp = false;
        }
        else
        {
            canMoveUp = true;
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerTileController>() != null && stunned == false)
        {
            playerFinded = true;
        }
    }
}
