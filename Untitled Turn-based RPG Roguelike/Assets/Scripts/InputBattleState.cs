using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        Debug.Log("InputBattleState.OnEnter() called");

        //TODO: Display battle gui

    }

    public void OnExit(BattleStateController controller)
    {
        Debug.Log("InputBattleState.OnExit() called");
        //TODO: Hide battle gui
    }

    public void UpdateState(BattleStateController controller)
    {
        Debug.Log("InputBattleState.OnUpdateState() called");
        //Todo: When user hits confirm
        if (UIManager.Instance.currentCommand is ConfirmBCom)
        {
            controller.ChangeState(controller.executeBattleState);
        }
    }

 
 
}
