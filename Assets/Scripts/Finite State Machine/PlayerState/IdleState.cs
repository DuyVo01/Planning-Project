using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundState
{
    private Vector3 stopForce;

    public IdleState(StateDependencies stateDependencies) : base(stateDependencies)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.SetAnimationBool(AnimationParameter.IDLE, true);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetAnimationBool(AnimationParameter.IDLE, false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.AddForce(stopForce, ForceMode.Acceleration);
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

        if(player.GetCurrentSpeed() != Vector3.zero)
        {
            EliminateRemainingSpeed();
        }
    }

    private void EliminateRemainingSpeed()
    {
        Vector3 targetSpeedVector = playerInput.MovementVector * playerData.maxRunSpeed;
        Vector3 neededForce = targetSpeedVector - player.GetCurrentSpeed();

        stopForce = neededForce * playerData.speedDecelAmount;
    }
}
