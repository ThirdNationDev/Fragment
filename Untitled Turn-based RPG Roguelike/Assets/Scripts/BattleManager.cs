/* Copyright (C) 2023 Thomas Payne, Third Nation Games - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Third Nation Games license, which unfortunately won't be
 * written for another century.
 *
 * You should have received a copy of the Third Nation Games license with
 * this file. If not, please write to: dev@thirdnationgames.com, or visit : www.thirdnationgames.com
 */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using System;
using System.Collections;
using System.Collections.Generic;
using Utils;

using UnityEngine;
using UnityEngine.Assertions;

using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    //Just for during development. These values will be passed during actual gameplay.
    public Combatant[] combatantPrefabs;

    public int enemyStartingZone;
    public int playerStartingZone;
    public int turnCtr;
    private List<Combatant> combatantsInTurnOrder;
    private CommandManager commandManager;
    private List<Combatant> defeatedCombatants;
    public static BattleManager Instance { get; private set; }
    public Battlefield battlefield { get; private set; }

    public Combatant currentCombatant
    {
        get { return combatantsInTurnOrder[0]; }
        private set { }
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

    private void ActivateBattleCommandUI()
    {
        Debug.Log("Activating the Battle Command UI");
        UIManager.Instance.ActivateBattleUI();
    }

    private void AdvanceTurnCountdown()
    {
        Combatant lastCombatant = currentCombatant;
        int timePassed = currentCombatant.countdownToTurn;
        Assert.AreNotEqual(timePassed, 0);

        foreach (Combatant c in combatantsInTurnOrder)
        {
            c.countdownToTurn -= timePassed;
            Assert.IsTrue((c.countdownToTurn >= 0) && (c.countdownToTurn <= 100));
        }

        currentCombatant.countdownToTurn = 100;
        combatantsInTurnOrder.Sort();
        Assert.AreNotEqual(lastCombatant, currentCombatant);
    }

    private void Awake()
    {
        //Singleton instantiation
        if (Instance != null && Instance != this)
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

    private bool PlayerWon()
    {
        Debug.Log("Checking if the player won");

        Assert.IsNotNull(combatantsInTurnOrder);
        Assert.AreNotEqual(combatantsInTurnOrder.Count, 0);

        int enemyCount = 0;
        foreach (Combatant c in combatantsInTurnOrder)
        {
            if (c is EnemyCombatant)
            {
                enemyCount++;
            }
        }
        return enemyCount == 0;
    }

    private void PlayOpeningCinematics()
    {
        Debug.Log("Playing the Opening Cinematics");
    }

    private void PrepareBattlefield()
    {
        Debug.Log("Preparing the Battlefield");
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

    private void Start()
    {
        PrepareBattlefield();
        InstantiateCombatants();
        PrepareCombatantsForBattle();
        PlayOpeningCinematics();
        ActivateBattleCommandUI();
    }

    private void StartLossSequence()
    {
        Debug.Log("Starting the Loss Sequence");
    }

    private void StartVictorySequence()
    {
        Debug.Log("Starting the won sequence.");
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
}