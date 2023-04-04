using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState 
{
    public RunState(PlayerController2D playerController2D)
    {
        owner = playerController2D;
    }
    public override void Enter()
    {
       base.Enter();
        Debug.Log("Sto correndo");
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Execute()
    {
        owner.animator.SetFloat("velocity", Mathf.Abs(owner.velocity));

        if (owner.velocity == 0)
        {
            owner.stateMachine.SetState(new DefaultState(owner));

        }
        

    }
}

    
    

