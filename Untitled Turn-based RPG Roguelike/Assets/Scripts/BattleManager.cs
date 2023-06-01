using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public Combatant[] combatantPrefabs;
    private Combatant[] combatants;
    public float damageBuffer;
    public int numCommandsToExecute;

    internal float CalculateDamage(float damageDealt, Combatant combatTarget)
    {
        return damageDealt * (damageBuffer / (damageBuffer + combatTarget.stats.discipline));
    }

    public BattleStateController battleStateController { get; private set; }
    public Battlefield battlefield { get; private set; }
    public Stack<BattleCommand> executedCommandStack;
    public Queue<BattleCommand> commandsToExecuteQueue;



    public int playerStartingZone;
    public int enemyStartingZone;


    public int turnCtr;
    int combatantIndex;

    public Combatant currentCombatant => combatants[combatantIndex];


    internal void nextCombatant()
    {
        if(combatants.Length > 0)
        {
            do
            {
                combatantIndex++;
                if (combatantIndex >= combatants.Length || combatantIndex < 0)
                {
                    combatantIndex = 0;
                }
            } while (!combatants[combatantIndex].isActiveAndEnabled);
        }
        
    }

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
        executedCommandStack = new Stack<BattleCommand>();
        commandsToExecuteQueue = new Queue<BattleCommand>();

        turnCtr = 0;
        combatantIndex = 0;

    }

    public void PlaceCombatants()
    {
        combatants = new Combatant[combatantPrefabs.Length];
        for(int i = 0; i < combatantPrefabs.Length; i++)
        {
            combatants[i] = Instantiate(combatantPrefabs[i]).GetComponent<Combatant>();
        }
        
        battlefield.PlaceCombatants(combatants);
    }

    public void CreateBattlefield()
    {
        battlefield.CreateBattlefield();
    }

    private void Update()
    {
        numCommandsToExecute = commandsToExecuteQueue.Count;
    }
}
