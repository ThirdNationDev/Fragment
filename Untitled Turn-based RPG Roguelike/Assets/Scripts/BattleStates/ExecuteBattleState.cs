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
        //TODO: Move on to next combatant in queue

    }

    public void UpdateState(BattleStateController controller)
    {
        //Debug.Log("ExecuteBattleState.UpdateState() called.");
        //TODO: When cinematics are done, move to next character in queue
        //if all enemy combatants dead, you won!
        //controller.ChangeState(controller.wonBattleState);
        //if all player characters dead, you lose!
        //controller.ChangeState(controller.lostBattleState);
        //otherwise, move to next combatant in queue and start the next turn
        
         //controller.ChangeState(controller.startTurnBattleState);

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
