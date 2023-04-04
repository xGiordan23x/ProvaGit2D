using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController2D : MonoBehaviour
{
    
   public static PlayerController2D Instance;

    [Header("Variables")]
    public StateMachine stateMachine;   
    public Animator animator;
    public Rigidbody2D rb;
    private Vector3 scale;
   
    [Header("Movement")]
    public float velocity;
    [SerializeField] private float speed;

    [Header("Jump")]
    public float jumpForce;
    public bool isGrounded;
    public bool isJumping;
    public UnityEngine.Transform groundChecker;
    public LayerMask groundMask;
   




    void Start()
    {
        stateMachine = new StateMachine(new DefaultState(this));
       scale = transform.localScale;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();


        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
       velocity = horizontal * speed;
       Flip(horizontal);  
        transform.Translate(velocity, 0, 0);


        CheckForGround();

        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            Jump();
        }
       
       

        stateMachine.StateUpdate();
    }
    public void Jump()
    {
       
        isJumping = true;
        animator.SetBool("isJumping", isJumping);
        
        stateMachine.SetState(new JumpState(this));
        
    }

    public void CheckForGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, 0.3f, groundMask);
        animator.SetBool("isGrounded", isGrounded);

       
    }
    public void Flip(float velocity)
    {
        if(velocity > 0)
        {
            Vector3 temp = transform.localScale;
            temp.x = scale.x;
            transform.localScale = temp;         
        }
        else if(velocity < 0) 
        {
            Vector3 temp = transform.localScale;
            temp.x = -scale.x;
            transform.localScale = temp;
            
        }
    }
}
