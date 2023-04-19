using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerController2D _playerController;
    public PlayerAttackState(PlayerController2D playerController)
    {
        _playerController = playerController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Sto attaccando");
        _playerController.animator.SetTrigger("Attack");
        Invoke(nameof(StopAttack), 3f);

    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        //quando animazione finisce passare a idle
       
    }
    public void StopAttack()
    {
        _playerController.StateMachine.SetState(PLayerStateType.Idle);
    }

}
