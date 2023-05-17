using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        Debug.Log("StartTurnBattleState.OnEnter() called.");

        //TODO: Cinematics on combatant
        //TODO: Starting effects (poison, etc)
        controller.turnCounter++;
    }

    public void OnExit(BattleStateController controller)
    {
        Debug.Log("StartTurnBattleState.OnExit() called.");
        //Nothing to do here
    }

    public void UpdateState(BattleStateController controller)
    {
        Debug.Log("StartTurnBattleState.UpdateState() called.");
        //TODO: Make sure on enter effects are done
        controller.ChangeState(controller.inputBattleState);
    }

 
 
}
