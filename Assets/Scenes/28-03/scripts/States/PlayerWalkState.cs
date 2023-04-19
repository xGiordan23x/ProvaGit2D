using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IState 
{
    private PlayerController2D _playerController;
    public PlayerWalkState(PlayerController2D playerController)
    {   
        _playerController = playerController;
    }

    public override void OnEnter()
    {
       base.OnEnter();
        Debug.Log("Sto correndo");
        _playerController.animator.SetTrigger("Walk");
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if(horizontal == 0 )
        {
            _playerController.StateMachine.SetState(PLayerStateType.Idle);
            return;
        }

        _playerController.FlipSprite(horizontal);
        horizontal *= Time.deltaTime * _playerController.WalkSpeed;
        _playerController.transform.Translate(horizontal,0,0);

        if (Input.GetButtonDown("Jump") && _playerController.isGrounded)
        {
            _playerController.StateMachine.SetState(PLayerStateType.Jump);
        }
        if (Input.GetButtonDown("Fire1") && _playerController.isGrounded)
        {
            
            _playerController.StateMachine.SetState(PLayerStateType.Attack);
        }
    }
}

    
    

