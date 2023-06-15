using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CombatantTestSuite
{
    private GameObject enemyCombatantPrefab = Resources.Load<GameObject>("Prefabs/EnemyPrefab");
    private GameObject playerCombatantPrefab = Resources.Load<GameObject>("Prefabs/Pinky");

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Scenes/BattleDev v0-1");
        yield return null;
    }

    [Test]
    public void TestCanMoveTo()
    {
        Combatant combatant = BattleManager.Instance.CurrentCombatant;
        int range = combatant.Stats.maxRange;
        int zoneNumber = combatant.battlezone.zoneNumber;
        Battlezone[] zonesInMoveRange = BattleManager.Instance.battlefield.GetZones(zoneNumber - range, zoneNumber + range);
        foreach (Battlezone zone in zonesInMoveRange)
        {
            Assert.IsTrue(combatant.TestCanMoveTo(zone));
        }
    }

    [Test]
    public void TestCompareTo_Enemy()
    {
        Combatant combatant1 = GameObject.Instantiate(enemyCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant2 = GameObject.Instantiate(enemyCombatantPrefab).GetComponent<Combatant>();
        combatant1.countdownToTurn = 1;
        combatant2.countdownToTurn = 10;
        int comparison = combatant1.CompareTo(combatant2);
        Assert.IsTrue(comparison < 0);
    }

    [Test]
    public void TestCompareTo_Player()
    {
        Combatant combatant1 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Combatant combatant2 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        combatant1.countdownToTurn = 1;
        combatant2.countdownToTurn = 10;
        int comparison = combatant1.CompareTo(combatant2);
        Assert.IsTrue(comparison < 0);
    }

    [Test]
    public void TestDeath()
    {
        Combatant combatant = BattleManager.Instance.CurrentCombatant;
        combatant.Death();
        Assert.IsFalse(combatant.gameObject.activeInHierarchy);
    }

    [Test]
    public void TestEndTurn()
    {
        Combatant combatant = BattleManager.Instance.CurrentCombatant;
        combatant.EndTurn();
        Assert.IsTrue(combatant.Particles.isStopped);
    }

    [Test]
    public void TestInitialize_Enemy()
    {
        Combatant combatant = GameObject.Instantiate(enemyCombatantPrefab).GetComponent<Combatant>();
        combatant.TestWrapperInitialize();

        Assert.AreEqual(combatant.Stats.AP, combatant.Stats.startingAP);
        Assert.AreEqual(combatant.Stats.health, combatant.Stats.maxHealth);
    }

    [Test]
    public void TestInitialize_Player()
    {
        Combatant combatant = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        combatant.TestWrapperInitialize();
        Assert.AreEqual(combatant.Stats.AP, combatant.Stats.startingAP);
        Assert.AreEqual(combatant.Stats.health, combatant.Stats.maxHealth);
    }

    [Test]
    public void TestReceiveDamage()
    {
        Combatant combatant = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        float startingHealth = combatant.Stats.health;
        float damage = 1;
        combatant.ReceiveDamage(damage);
        Assert.AreEqual(startingHealth - damage, combatant.Stats.health);
    }

    [Test]
    public void TestStartTurn()
    {
        Combatant combatant = BattleManager.Instance.CurrentCombatant;
        combatant.EndTurn();
        combatant.StartTurn();
        Assert.IsTrue(combatant.Particles.isEmitting);
        Assert.IsTrue(combatant.zonesInMoveRange.Length > 0);
    }
}