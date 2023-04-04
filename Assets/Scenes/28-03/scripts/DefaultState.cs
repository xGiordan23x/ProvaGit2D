using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : IState
{
    
    public DefaultState(PlayerController2D playerController2D)
    {
        owner = playerController2D;
    }

    public override void Enter()
    {
       base.Enter();
        Debug.Log("Sono in idle");
        
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Execute()
    {
        
        if(owner.velocity >= 0.01 || owner.velocity <= -0.01)
        {
            owner.stateMachine.SetState(new RunState(owner));           
        }
       
    }
}
