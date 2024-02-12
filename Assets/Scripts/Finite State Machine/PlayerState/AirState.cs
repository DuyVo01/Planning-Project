using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerBaseState
{
    private bool toGround;
    public AirState(StateDependencies stateDependencies) : base(stateDependencies)
    {
        ANIM = "jumpUp";
        MessageBroker.Instance.Subscribe(MessageEventName.ON_LANDING_END, DonePlayingLandingAnim);
    }

    public override void Enter()
    {
        base.Enter();
        toGround = false;
        player.SetAnimationBool(ANIM, true);

    }

    public override void Exit()
    {
        base.Exit();
        player.SetAnimationBool(ANIM, false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void Update()
    {
        base.Update();

        if(player.GetCurrentSpeed().y < 0)
        {
            player.GroundCheck();
        }

        if (player.isGrounded)
        {
            if(playerInput.MovementVector != Vector3.zero)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (toGround)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }

        player.SetAnimationFloat("yVelocity", player.GetCurrentSpeed().y);
    }

    private void DonePlayingLandingAnim(object eventData)
    {
        toGround = true;
    }
}
