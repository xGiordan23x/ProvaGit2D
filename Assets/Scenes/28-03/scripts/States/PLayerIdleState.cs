using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerIdleState : IState
{
    private PlayerController2D _playerController;
    public PLayerIdleState(PlayerController2D playerController)
    {
        _playerController= playerController;
    }

    public override void OnEnter()
    {
       base.OnEnter();
        Debug.Log("Sono in Idle");
        _playerController.animator.SetTrigger("Idle");
        
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {

       if(!_playerController.isGrounded)
        {
            _playerController.StateMachine.SetState(PLayerStateType.Fall);
            return;
        }
        if(Input.GetAxis("Horizontal") != 0f)
        {
            _playerController.StateMachine.SetState(PLayerStateType.Walk);
        }
        if (Input.GetButtonDown("Jump") && _playerController.isGrounded)
        {
            _playerController.StateMachine.SetState(PLayerStateType.Jump);
        }
        if(Input.GetButtonDown("Fire1") && _playerController.isGrounded)
        {
            _playerController.StateMachine.SetState(PLayerStateType.Attack);
        }
       
    }
}
