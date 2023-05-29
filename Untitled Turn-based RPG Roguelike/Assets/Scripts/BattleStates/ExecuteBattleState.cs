using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteBattleState : IBattleState
{
    public void OnEnter(BattleStateController controller)
    {
        Debug.Log("ExecuteBattleState.OnEnter() called.");
        //TODO: start cinematics of attack
    }

    public void OnExit(BattleStateController controller)
    {
        Debug.Log("ExecuteBattleState.OnExit() called.");
        //TODO: Push commands onto queue
        BattleManager.Instance.turnCtr++;
        BattleManager.Instance.currentCombatant.EndTurn();
        BattleManager.Instance.nextCombatant();


    }

    public void UpdateState(BattleStateController controller)
    {


        //CHeck if there is a command
        if(BattleManager.Instance.currentCommand != null)
        {
            BattleCommand command = BattleManager.Instance.currentCommand;
            command.Execute();
            BattleManager.Instance.currentCommand = null;
            if (command.endsTurn)
            {
                controller.ChangeState(controller.startTurnBattleState);
            }
        }
        //Check if it is a turn ending command
        //
        

    }



 
 
}
