using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class CombatantManager
{
    private readonly int MAX_COUNTDOWN = 100;
    private List<Combatant> combatantsInTurnOrder;
    private List<Combatant> defeatedCombatants;

    public CombatantManager()
    {
        combatantsInTurnOrder = new List<Combatant>();
        defeatedCombatants = new List<Combatant>();
    }

    public Combatant CurrentCombatant
    {
        get
        {
            Assert.IsTrue(combatantsInTurnOrder.Count > 0);
            return combatantsInTurnOrder[0];
        }
    }

    public List<Combatant> CombatantsInTurnOrderCopy()
    {
        return new List<Combatant>(combatantsInTurnOrder);
    }

    public List<Combatant> DefeatedCombatantsCopy()
    {
        return new List<Combatant>(defeatedCombatants);
    }

    public int AlivePlayerCount()
    {
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

        return playerCount;
    }

    public int AliveEnemyCount()
    {
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

        return enemyCount;
    }

    public void AddCombatant(Combatant combatant)
    {
        combatantsInTurnOrder.Add(combatant);
        combatantsInTurnOrder.Sort();
    }

    public void SetDefeated(Combatant combatant)
    {
        Assert.IsNotNull(combatant);
        Assert.IsNotNull(combatantsInTurnOrder);
        Assert.IsNotNull(defeatedCombatants);
        Assert.IsTrue(combatantsInTurnOrder.Contains(combatant));

        combatant.gameObject.SetActive(false);
        combatantsInTurnOrder.Remove(combatant);
        defeatedCombatants.Add(combatant);
    }

    public void ReadyFirstTurn()
    {
        SetInitialTurnOrder();
        CurrentCombatant.StartTurn();
    }

    public void StartTheNextCombatantTurn()
    {
        Assert.IsNotNull(CurrentCombatant);

        CurrentCombatant.EndTurn();
        AdvanceTurnCountdown();
        CurrentCombatant.StartTurn();
    }

    private void SetInitialTurnOrder()
    {
        Assert.IsNotNull(combatantsInTurnOrder);

        Stack<int> initialCountdownValues = BattleCalculator.RandomNonrepeatingInts(1, MAX_COUNTDOWN, combatantsInTurnOrder.Count);

        foreach (Combatant c in combatantsInTurnOrder)
        {
            c.countdownToTurn = initialCountdownValues.Pop();
        }

        combatantsInTurnOrder.Sort();
    }

    private void AdvanceTurnCountdown()
    {
        //Move current to back and resort
        Combatant lastCombatant = CurrentCombatant;
        CurrentCombatant.countdownToTurn = MAX_COUNTDOWN;
        combatantsInTurnOrder.Sort();
        Assert.AreNotEqual(lastCombatant, CurrentCombatant);

        //Advance the countdown
        int timePassed = CurrentCombatant.countdownToTurn;

        foreach (Combatant c in combatantsInTurnOrder)
        {
            c.countdownToTurn -= timePassed;
            Assert.IsTrue((c.countdownToTurn >= 0) && (c.countdownToTurn <= 100));
        }

        Assert.AreEqual(0, CurrentCombatant.countdownToTurn);
    }
}