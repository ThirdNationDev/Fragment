using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        Debug.Log("LostBattleState.OnEnter() called.");
        //Return to homebase
        //Dismantale state machine
        throw new System.NotImplementedException();
    }

    public void OnExit(BattleStateController controller)
    {
        Debug.Log("LostBattleState.OnExit() called.");
        //Should never be called
        throw new System.NotImplementedException();
    }

    public void UpdateState(BattleStateController controller)
    {
        Debug.Log("LostBattleState.UpdateState() called.");
        //Should never be called
        throw new System.NotImplementedException();
    }

 
 
}
