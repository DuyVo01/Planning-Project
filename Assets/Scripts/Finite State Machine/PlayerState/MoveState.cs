using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundState
{
    private Vector3 speedForce;

    public MoveState(StateDependencies stateDependencies) : base(stateDependencies)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.SetAnimationBool(AnimationParameter.MOVE, true);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetAnimationBool(AnimationParameter.MOVE, false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.AddForce(speedForce, ForceMode.Acceleration);
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void Update()
    {
        base.Update();
        if(playerInput.MovementVector == Vector3.zero)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        CalculateMoveSpeed();
    }

    private void CalculateMoveSpeed()
    {
        Vector3 targetSpeedVector = playerInput.MovementVector * playerData.maxRunSpeed;
        Vector3 neededForce = targetSpeedVector - player.GetCurrentSpeed();

        speedForce = neededForce * playerData.speedAccelAmount;
    }

    
}


