using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CombatantManagerTestSuite
{
    private CombatantManager combatantManager;
    private GameObject playerCombatantPrefab = Resources.Load<GameObject>("Prefabs/Pinky");
    private GameObject enemyCombatantPrefab = Resources.Load<GameObject>("Prefabs/EnemyPrefab");

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        combatantManager = new CombatantManager();
        SceneManager.LoadScene("Scenes/BattleDev v0-1");
        yield return null;
    }

    [Test]
    public void TestCurrentCombatant()
    {
        Combatant combatant1 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant2 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        combatant1.countdownToTurn = 20;
        combatant2.countdownToTurn = 40;

        combatantManager.AddCombatant(combatant2);
        combatantManager.AddCombatant(combatant1);

        Assert.AreEqual(combatantManager.CurrentCombatant, combatant1);
    }

    [Test]
    public void TestAddCombatant()
    {
        Combatant combatant1 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant2 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant3 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant4 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();

        combatantManager.AddCombatant(combatant1);
        combatantManager.AddCombatant(combatant2);
        combatantManager.AddCombatant(combatant3);
        combatantManager.AddCombatant(combatant4);

        List<Combatant> addedCombatants = combatantManager.CombatantsInTurnOrderCopy();

        Assert.IsTrue(addedCombatants.Contains(combatant1)
             && addedCombatants.Contains(combatant2)
             && addedCombatants.Contains(combatant3)
             && addedCombatants.Contains(combatant4));
    }

    [Test]
    public void TestSetDefeated()
    {
        Combatant combatant1 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant2 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();

        combatantManager.AddCombatant(combatant1);
        combatantManager.AddCombatant(combatant2);

        combatantManager.SetDefeated(combatant2);

        List<Combatant> aliveCombatants = combatantManager.CombatantsInTurnOrderCopy();
        List<Combatant> deadCombatants = combatantManager.DefeatedCombatantsCopy();

        Assert.IsTrue(aliveCombatants.Contains(combatant1) && !aliveCombatants.Contains(combatant2)
            && deadCombatants.Contains(combatant2) && !deadCombatants.Contains(combatant1));
    }

    [Test]
    public void TestStartTheNextCombatantTurn()
    {
        List<Combatant> combatants = BattleManager.Instance.CombatantsInTurnOrderCopy;

        BattleManager.Instance.StartTheNextCombatantTurn();

        Assert.AreEqual(combatants[1], BattleManager.Instance.CurrentCombatant);
    }

    [Test]
    public void TestReadyFirstTurn()
    {
        Combatant combatant1 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant2 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant3 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant4 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();

        combatantManager.AddCombatant(combatant1);
        combatantManager.AddCombatant(combatant2);
        combatantManager.AddCombatant(combatant3);
        combatantManager.AddCombatant(combatant4);

        Battlezone zone = BattleManager.Instance.battlefield.GetZone(0);

        combatant1.battlezone = zone;
        combatant2.battlezone = zone;
        combatant3.battlezone = zone;
        combatant4.battlezone = zone;

        combatantManager.ReadyFirstTurn();

        List<Combatant> orderedCombatants = combatantManager.CombatantsInTurnOrderCopy();

        Assert.IsTrue(orderedCombatants[0].countdownToTurn <= orderedCombatants[1].countdownToTurn
            && orderedCombatants[1].countdownToTurn <= orderedCombatants[2].countdownToTurn
            && orderedCombatants[3].countdownToTurn <= orderedCombatants[3].countdownToTurn);
    }

    [Test]
    public void TestAlivePlayerCount()
    {
        Combatant pCombatant1 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant pCombatant2 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant eCombatant1 = GameObject.Instantiate(enemyCombatantPrefab).GetComponent<Combatant>();

        combatantManager.AddCombatant(pCombatant1);
        combatantManager.AddCombatant(pCombatant2);
        combatantManager.AddCombatant(eCombatant1);

        combatantManager.SetDefeated(pCombatant2);

        Assert.AreEqual(1, combatantManager.AlivePlayerCount());
    }

    [Test]
    public void TestAliveEnemyCount()
    {
        Combatant pCombatant1 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant eCombatant1 = GameObject.Instantiate(enemyCombatantPrefab).GetComponent<Combatant>();
        Combatant eCombatant2 = GameObject.Instantiate(enemyCombatantPrefab).GetComponent<Combatant>();

        combatantManager.AddCombatant(pCombatant1);
        combatantManager.AddCombatant(eCombatant1);
        combatantManager.AddCombatant(eCombatant2);

        combatantManager.SetDefeated(eCombatant2);

        Assert.AreEqual(1, combatantManager.AliveEnemyCount());
    }
}