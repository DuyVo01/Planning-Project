using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : PlayerBaseState
{
    private bool isJumping = false;
    public GroundState(StateDependencies stateDependencies) : base(stateDependencies)
    {
        MessageBroker.Instance.Subscribe(MessageEventName.ON_JUMP, Jump);
    }

    public override void Enter()
    {
        base.Enter();
        isJumping = false;
    }

    public override void Exit()
    {
        base.Exit();
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
        if (!isJumping)
        {
            player.GroundCheck();
        }
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.AirState);
        }
    }

    public void Jump(object doJump)
    {
        if (!isActiveState)
        {
            return;
        }

        player.AddForce(new Vector3(0, playerData.jumpForce, 0), ForceMode.Impulse);
        isJumping = true;
    }
}
