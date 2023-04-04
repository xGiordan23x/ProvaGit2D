using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    public JumpState(PlayerController2D playerController2D)
    {
        owner = playerController2D;
    }
    public override void Enter()
    {
        Debug.Log("Sto saltando");

        owner.rb.AddForce(Vector2.up * owner.jumpForce, ForceMode2D.Impulse);
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Execute()
    {
        
        if(owner.rb.velocity.y <=0)
        {
            owner.isJumping = false;
            owner.animator.SetBool("isJumping", owner.isJumping);

            if (owner.isGrounded)
            {
                owner.stateMachine.SetState(new DefaultState(owner));
            }
        }

        
       
    }
}
