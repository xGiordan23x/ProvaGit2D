using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    private PlayerController2D _playerController;
    public PlayerJumpState(PlayerController2D playerController)
    {
        _playerController = playerController;
    }

    public override void OnEnter()
    {
        Debug.Log("Sto saltando");
        _playerController.rb.AddForce(Vector2.up * _playerController.jumpForce, ForceMode2D.Impulse );
        _playerController.animator.SetTrigger("Jump");
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        _playerController.HorizontalMovement();
        if (_playerController.rb.velocity.y < 0) 
        {
            _playerController.StateMachine.SetState(PLayerStateType.Fall);
        }



    }
}
