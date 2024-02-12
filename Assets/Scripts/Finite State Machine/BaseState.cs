

public abstract class BaseState 
{
    protected StateMachine stateMachine;
    protected IStateOwner stateOwner;
    protected PlayerInput playerInput;
    protected PlayerDataSO playerData;
    protected string ANIM;

    public bool isExiting { get; protected set; }

    public BaseState (StateDependencies stateDependencies)
    {
        stateMachine = stateDependencies.StateMachine;
        stateOwner = stateDependencies.StateOwner;
        playerInput = stateDependencies.PlayerInput;
        playerData = stateDependencies.PlayerData;
    }

    public virtual void Enter()
    {
        isExiting = false;
    }
    public virtual void Exit()
    {
        isExiting = true;
    }
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void LateUpdate();
}
