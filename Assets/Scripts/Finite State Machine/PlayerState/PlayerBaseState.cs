using UnityEngine;

public class PlayerBaseState : BaseState
{
    protected Player player;
    public PlayerBaseState(StateDependencies stateDependencies) : base(stateDependencies)
    {
        player = stateOwner as Player;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter " + this.GetType().Name);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void FixedUpdate()
    {
        
    }

    public override void LateUpdate()
    {
        
    }

    public override void Update()
    {
        
    }
}
