using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        Debug.Log("WonBattleState.OnEnter() called");
        //Display cinematics
        //Display menu
        //Display input options
    }

    public void OnExit(BattleStateController controller)
    {
        Debug.Log("WonBattleState.OnExit() called");
        //Hide gui
        //Save stats?
    }

    public void UpdateState(BattleStateController controller)
    {
        Debug.Log("WonBattleState.OnUpdateState() called");
        //When user hits confirm, end the battle
        controller.ChangeState(controller.returnBattleState);
    }

 
 
}
