using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        Debug.Log("ReturnBattleState.OnEnter() called.");
        //Return to dungeon or wherever
        //End state machine
    }

    public void OnExit(BattleStateController controller)
    {
        Debug.Log("ReturnBattleState.OnExit() called.");
        //Should not be called
        throw new System.NotImplementedException();
    }

    public void UpdateState(BattleStateController controller)
    {
        Debug.Log("ReturnBattleState.UpdateState() called.");
        //Should not be called
        throw new System.NotImplementedException();
    }

 
 
}
