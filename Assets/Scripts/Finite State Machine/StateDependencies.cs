using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDependencies 
{
    public PlayerInput PlayerInput { get; private set; }
    public IStateOwner StateOwner { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public PlayerDataSO PlayerData { get; private set; }

    public StateDependencies(IStateOwner stateOwner, StateMachine stateMachine, PlayerInput playerInput, PlayerDataSO playerDataSO)
    {
        StateOwner = stateOwner;
        StateMachine = stateMachine;
        PlayerInput = playerInput;
        PlayerData = playerDataSO;
    }
}
