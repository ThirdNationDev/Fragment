using System;
using System.Collections;
using System.Collections.Generic;
using Utils;

using UnityEngine;
using UnityEngine.Assertions;

using Random = UnityEngine.Random;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    private List<Combatant> combatantsInTurnOrder;
    private List<Combatant> defeatedCombatants;
    private CommandManager commandManager;
    public Battlefield battlefield { get; private set; }
    public int turnCtr;
    public Combatant currentCombatant
    {
        get { return combatantsInTurnOrder[0]; }
        private set { }
    }

    //Just for during development. These values will be passed during actual gameplay.
    public Combatant[] combatantPrefabs;
    public int playerStartingZone;
    public int enemyStartingZone;

    private void Awake()
    {
        //Singleton instantiation
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        battlefield = GetComponentInChildren<Battlefield>();
        commandManager = GetComponent<CommandManager>();
        combatantsInTurnOrder = new List<Combatant>();
        defeatedCombatants = new List<Combatant>();


        turnCtr = 0;
    }

    private void Start()
    {
        PrepareBattlefield();
        InstantiateCombatants();
        PrepareCombatantsForBattle();
        PlayOpeningCinematics();
        ActivateBattleCommandUI();
    }
    private void Update()
    {
        if (PlayerWon())
        {
            StartVictorySequence();
        }
        else if (PlayerLost())
        {
            StartLossSequence();
        }

    }

    private void InstantiateCombatants()
    {
        Debug.Log("Instantiating the combatants");
        Assert.IsNotNull(combatantPrefabs);
        Assert.AreNotEqual(combatantPrefabs.Length, 0);

        for (int i = 0; i < combatantPrefabs.Length; i++)
        {
            combatantsInTurnOrder.Add(Instantiate(combatantPrefabs[i]).GetComponent<Combatant>());
        }
    }

    private void StartLossSequence()
    {
        Debug.Log("Starting the Loss Sequence");    }

    private bool PlayerLost()
    {
        Debug.Log("Checking if the enemy won");

        Assert.IsNotNull(combatantsInTurnOrder);
        Assert.AreNotEqual(combatantsInTurnOrder.Count, 0);

        int playerCount = 0;
        foreach (Combatant c in combatantsInTurnOrder)
        {
            if (c is PlayerCombatant)
            {
                playerCount++;
            }
        }
        return playerCount == 0;
    }

    private void StartVictorySequence()
    {
        Debug.Log("Starting the won sequence.");
    }

    private bool PlayerWon()
    {
        Debug.Log("Checking if the player won");

        Assert.IsNotNull(combatantsInTurnOrder);
        Assert.AreNotEqual(combatantsInTurnOrder.Count, 0);

        int enemyCount = 0;
        foreach(Combatant c in combatantsInTurnOrder)
        {
            if(c is EnemyCombatant)
            {
                enemyCount++;
            }
        }
        return enemyCount == 0;
    }

    private void ActivateBattleCommandUI()
    {
        Debug.Log("Activating the Battle Command UI");
        UIManager.Instance.ActivateBattleUI();
    }

    private void PlayOpeningCinematics()
    {
        Debug.Log("Playing the Opening Cinematics");
    }

    private void PrepareCombatantsForBattle()
    {
        Debug.Log("Preparing the Combatants for Battle");
        battlefield.PlaceCombatants(combatantsInTurnOrder);
        SetInitialTurnOrder();
        currentCombatant.StartTurn();
    }

    private void SetInitialTurnOrder()
    {
        foreach (Combatant c in combatantsInTurnOrder)
        {
            c.countdownToTurn = (int)Random.Range(1, 100);
        }

        combatantsInTurnOrder.Sort();

    }

    private void PrepareBattlefield()
    {
        Debug.Log("Preparing the Battlefield");
    }

    public void CreateBattlefield()
    {
        battlefield.CreateBattlefield();
    }


    internal void StartTheNextCombatantTurn()
    {
        Assert.IsNotNull(combatantsInTurnOrder);
        Assert.AreNotEqual(combatantsInTurnOrder.Count, 0);

        currentCombatant.EndTurn();
        AdvanceTurnCountdown();
        currentCombatant.StartTurn();
    }

    private void AdvanceTurnCountdown()
    {
        Combatant lastCombatant = currentCombatant;
        int timePassed = currentCombatant.countdownToTurn;
        Assert.AreNotEqual(timePassed, 0);

        foreach(Combatant c in combatantsInTurnOrder)
        {
            c.countdownToTurn -= timePassed;
            Assert.IsTrue((c.countdownToTurn >= 0) && (c.countdownToTurn <= 100));
        }

        currentCombatant.countdownToTurn = 100;
        combatantsInTurnOrder.Sort();
        Assert.AreNotEqual(lastCombatant, currentCombatant);
    }
}
