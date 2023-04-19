using Unity.VisualScripting;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    public static PlayerController2D Instance;

    [Header("Variables")]
    
    public Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    
    
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    public float WalkSpeed => walkSpeed;
    
    [Header("Jump")]
    public float jumpForce;
    public bool isGrounded
    {
        get
        {
            return (Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask)) ;
        }
    }
    
    [SerializeField] private float groundCheckDistance;

    public LayerMask groundMask;
    [Header("Attack")]
    public bool isAttacking;


    public StateMachine<PLayerStateType> StateMachine { get; } = new();




    void Start()
    {
        //Registriamo tutti gli stati possibili
        StateMachine.RegisterState(PLayerStateType.Idle, new PLayerIdleState(this));
        StateMachine.RegisterState(PLayerStateType.Walk, new PlayerWalkState(this));
        StateMachine.RegisterState(PLayerStateType.Fall, new PlayerFallState(this)); 
        StateMachine.RegisterState(PLayerStateType.Jump, new PlayerJumpState(this));
        StateMachine.RegisterState(PLayerStateType.Attack, new PlayerAttackState(this));


        //setto lo stato iniziale
        StateMachine.SetState(PLayerStateType.Idle);

       
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();


        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();

    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);
        //stateMachine.SetState(new AttackState(this));
    }

    
    
    public void FlipSprite(float speed)
    {
        spriteRenderer.flipX = speed < 0f;
    }

    public void HorizontalMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        FlipSprite(horizontal);
        horizontal *= Time.deltaTime * WalkSpeed;
        transform.Translate(horizontal, 0, 0);

    }
}
