using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : GroundState
{
    private bool toGround;

    public LandState(StateDependencies stateDependencies) : base(stateDependencies)
    {
        MessageBroker.Instance.Subscribe(MessageEventName.ON_LANDING_END, DonePlayingLandingAnim);
    }

    public override void Enter()
    {
        base.Enter();
        toGround = false;
        player.SetAnimationBool(AnimationParameter.LANDING, true);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetAnimationBool(AnimationParameter.LANDING, false);

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
        if(playerInput.MovementVector != Vector3.zero)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if(toGround)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private void DonePlayingLandingAnim(object eventData)
    {
        toGround = true;
        player.SetAnimationBool(AnimationParameter.LANDING, false);
    }
}
