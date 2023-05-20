using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateController : MonoBehaviour
{
    public GameObject combatantPrefab;
    public int turnCounter;
    
    IBattleState currentState;

    public StartBattleState startBattleState = new StartBattleState();
    public StartTurnBattleState startTurnBattleState = new StartTurnBattleState();
    public ExecuteBattleState executeBattleState = new ExecuteBattleState();
    public WonBattleState wonBattleState = new WonBattleState();
    public LostBattleState lostBattleState = new LostBattleState();
    public EscapedBattleState escapedBattleState = new EscapedBattleState();
    public ReturnBattleState returnBattleState = new ReturnBattleState();


    // Start is called before the first frame update
    void Start()
    {
        turnCounter = 0;
        ChangeState(startBattleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }

    }

    public void ChangeState(IBattleState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEnter(this);
    }
}

public interface IBattleState
{
    public void OnEnter(BattleStateController controller);
    public void UpdateState(BattleStateController controller);
    public void OnExit(BattleStateController controller);

}