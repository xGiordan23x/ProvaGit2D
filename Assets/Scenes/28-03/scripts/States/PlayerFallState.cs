using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IState
{
    private PlayerController2D _playerController;
    public PlayerFallState(PlayerController2D playerController)
    {
        _playerController = playerController;
    }

    public override void OnEnter()
    {
        Debug.Log("Sto Cadendo");
        _playerController.animator.SetTrigger("Fall");

    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        _playerController.HorizontalMovement();
        if (_playerController.isGrounded)
        {
            _playerController.StateMachine.SetState(PLayerStateType.Idle);
        }
    }



    }
