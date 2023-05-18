using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        Debug.Log("StartBattleState.OnEnter() called.");
        //Load battle environment
        //TODO: Create queue of combatants and organize by turn
        //TODO: POsition combatants on battle field
        //TODO: Opening cinematic and info
        BattleManager.Instance.CreateBattlefield();
        BattleManager.Instance.PlaceCombatants();
        
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
