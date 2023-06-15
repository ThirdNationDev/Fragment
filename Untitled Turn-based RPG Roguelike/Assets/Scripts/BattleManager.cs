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
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    //Just for during development. These values will be passed during actual gameplay.
    public Combatant[] combatantPrefabs;

    public int enemyStartingZone;
    public int playerStartingZone;
    public int turnCtr;
    private CombatantManager combatantManager;

    public static BattleManager Instance { get; private set; }
    public Battlefield battlefield { get; private set; }

    public List<Combatant> CombatantsInTurnOrderCopy { get => combatantManager.CombatantsInTurnOrderCopy(); }

    public Combatant CurrentCombatant
    {
        get => combatantManager.CurrentCombatant;
    }

    public List<Combatant> DefeatedCombatantsCopy { get => combatantManager.DefeatedCombatantsCopy(); }

    public void AddCombatantToBattle(Combatant combatant, Battlezone zone)
    {
        Assert.IsNotNull(combatant);
        Assert.IsNotNull(zone);

        combatantManager.AddCombatant(combatant);
        battlefield.PlaceCombatant(combatant, zone);
    }

    public void CreateBattlefield()
    {
        battlefield.CreateBattlefield();
    }

    public void SetDefeated(Combatant combatant)
    {
        Assert.IsNotNull(combatant);

        combatantManager.SetDefeated(combatant);
    }

    public void StartTheNextCombatantTurn()
    {
        combatantManager.StartTheNextCombatantTurn();
        turnCtr++;
    }

    private void ActivateBattleCommandUI()
    {
        Debug.Log("Activating the Battle Command UI");
        UIManager.Instance.ActivateBattleUI();
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

        Initialize();
    }

    private void Initialize()
    {
        battlefield = GetComponentInChildren<Battlefield>();
        combatantManager = new CombatantManager();
        turnCtr = 0;
    }

    private bool PlayerLost()
    {
        return combatantManager.AlivePlayerCount() == 0;
    }

    private bool PlayerWon()
    {
        return combatantManager.AliveEnemyCount() == 0;
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
        Assert.IsNotNull(combatantPrefabs);
        Assert.AreNotEqual(combatantPrefabs.Length, 0);

        Combatant combatant;
        Battlezone startingZone;

        for (int i = 0; i < combatantPrefabs.Length; i++)
        {
            combatant = Instantiate(combatantPrefabs[i]).GetComponent<Combatant>();
            if (combatant.tag == "Player")
            {
                startingZone = battlefield.GetZone(playerStartingZone);
            }
            else
            {
                startingZone = battlefield.GetZone(enemyStartingZone);
            }
            AddCombatantToBattle(combatant, startingZone);
        }

        combatantManager.ReadyFirstTurn();
    }

    private void Start()
    {
        PrepareBattlefield();
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