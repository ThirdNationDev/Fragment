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
        if(BattleManager.Instance.commandsToExecuteQueue.Count >0)
        {
            Debug.Log("There is a command to execute.");
            BattleCommand command = BattleManager.Instance.commandsToExecuteQueue.Dequeue();
            command.Execute();
            if (command.endsTurn)
            {
                controller.ChangeState(controller.startTurnBattleState);
            }
        }


        

    }



 
 
}
