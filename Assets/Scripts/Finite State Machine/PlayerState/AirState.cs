using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerBaseState
{
    public AirState(StateDependencies stateDependencies) : base(stateDependencies)
    {
        MessageBroker.Instance.Subscribe(MessageEventName.ON_JUMP_ANIM_END, DonePlayingJumpAnim);
    }

    public override void Enter()
    {
        base.Enter();

        player.SetAnimationBool(AnimationParameter.INAIR, true);


        if (player.GetCurrentSpeed().y >= 1)
        {
            player.SetAnimationBool(AnimationParameter.JUMP_UP, true);
        }
        else
        {
            player.SetAnimationBool(AnimationParameter.FALLING, true);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetAnimationBool(AnimationParameter.JUMP_UP, false);
        player.SetAnimationBool(AnimationParameter.FALLING, false);
        player.SetAnimationBool(AnimationParameter.INAIR, false);

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.ApplyGravity();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (player.GetCurrentSpeed().y < 1)
        {
            player.GroundCheck();
        }

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.LandState);
        }
    }

    private void DonePlayingJumpAnim(object eventData)
    {
        player.SetAnimationBool(AnimationParameter.JUMP_UP, false);

        player.SetAnimationBool(AnimationParameter.FALLING, true);
    }
}
