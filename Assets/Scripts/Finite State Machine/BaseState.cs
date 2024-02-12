

public abstract class BaseState 
{
    protected StateMachine stateMachine;
    protected IStateOwner stateOwner;
    protected PlayerInput playerInput;
    protected PlayerDataSO playerData;
    protected string ANIM;
    protected bool isActiveState;

    public bool isExiting { get; protected set; }

    public BaseState (StateDependencies stateDependencies)
    {
        stateMachine = stateDependencies.StateMachine;
        stateOwner = stateDependencies.StateOwner;
        playerInput = stateDependencies.PlayerInput;
        playerData = stateDependencies.PlayerData;

        isActiveState = false;
    }

    public virtual void Enter()
    {
        isActiveState = true;
        isExiting = false;
    }
    public virtual void Exit()
    {
        isActiveState = false;
        isExiting = true;
    }
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void LateUpdate();
}
