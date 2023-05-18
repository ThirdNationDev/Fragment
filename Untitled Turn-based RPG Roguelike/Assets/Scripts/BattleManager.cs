using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public Combatant[] combatants;
    public BattleStateController battleStateController { get; private set; }
    public Battlefield battlefield { get; private set; }
    public List<BattleCommand> commandList;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        battleStateController = GetComponent<BattleStateController>();
        battlefield = GetComponentInChildren<Battlefield>();
        commandList = new List<BattleCommand>();
    }

    public void PlaceCombatants()
    {
        battlefield.PlaceCombatants(combatants);
    }

    public void CreateBattlefield()
    {
        battlefield.CreateBattlefield();
    }

}
