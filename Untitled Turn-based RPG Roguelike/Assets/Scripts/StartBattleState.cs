using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        //Load battle environment
        //TODO: Create queue of combatants and organize by turn
        //TODO: POsition combatants on battle field
        //TODO: Opening cinematic and info
        Debug.Log("StartBattleState.OnEnter() called.");
    }

    public void OnExit(BattleStateController controller)
    {
        //Nothing to do here?
        Debug.Log("StartBattleState.OnExit() called.");
    }

    public void UpdateState(BattleStateController controller)
    {
        Debug.Log("StartBattleState.UpdateState() called.");
        controller.ChangeState(controller.startTurnBattleState);

    }

 
 
}
