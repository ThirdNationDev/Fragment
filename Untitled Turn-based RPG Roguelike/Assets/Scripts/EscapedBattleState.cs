using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapedBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        Debug.Log("EscapedBattleState.OnEnter() called.");
        //Bring up end of battle gui
    }

    public void OnExit(BattleStateController controller)
    {
        Debug.Log("EscapedBattleState.OnExit() called.");
        //hide GUI
        //Save stats
    }

    public void UpdateState(BattleStateController controller)
    {
        Debug.Log("EscapedBattleState.UpdateState() called.");
        //On confirm, end the battle
        controller.ChangeState(controller.returnBattleState);
    }

 
 
}
