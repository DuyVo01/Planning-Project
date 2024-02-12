
public class StateMachine
{
    private BaseState currentState;

    
    public void SetInitState(BaseState initState)
    {
        currentState = initState;
        currentState.Enter();
    }

    public void ChangeState(BaseState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState.isExiting)
        {
            return;
        }

        currentState.Update();
    }

    public void FixedUpdate()
    {
        if (currentState.isExiting)
        {
            return;
        }

        currentState.FixedUpdate();
    }

    public void LateUpdate() 
    {
        if (currentState.isExiting)
        {
            return;
        }
        currentState.LateUpdate(); 
    }
}
