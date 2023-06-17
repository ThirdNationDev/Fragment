using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BattlefieldTestSuite
{
    private GameObject enemyCombatantPrefab = Resources.Load<GameObject>("Prefabs/EnemyPrefab");
    private GameObject playerCombatantPrefab = Resources.Load<GameObject>("Prefabs/Pinky");
    private GameObject ResurrectSkillPrefab = Resources.Load<GameObject>("Prefabs/ResurrectSkill");

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Scenes/BattleDev v0-1");

        yield return null;
    }

    [Test]
    public void TestGetZone()
    {
        Debug.Log("TestGetZone called.");
        int zonenumber = 3;
        Battlezone zone3 = BattleManager.Instance.battlefield.GetZone(zonenumber);
        Debug.Log("Zone: " + zone3.zoneNumber);
        Debug.Log("Zone: " + zonenumber);

        Assert.AreEqual(zone3.zoneNumber, zonenumber);
    }

    [Test]
    public void TestGetZones()
    {
        Battlezone[] zones = BattleManager.Instance.battlefield.GetZones(3, 5);

        Assert.IsTrue(zones.Length == 3);
        Assert.IsTrue(zones[0].zoneNumber == 3);
        Assert.IsTrue(zones[1].zoneNumber == 4);
        Assert.IsTrue(zones[2].zoneNumber == 5);
    }

    [Test]
    public void TestPlaceCombatants()
    {
        int numCombatants1 = BattleManager.Instance.battlefield.getCombatants(0, BattleManager.Instance.battlefield.numZones - 1).Count;
        Combatant combatant1 = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
        Battlezone startzone = BattleManager.Instance.battlefield.GetZone(0);
        BattleManager.Instance.battlefield.PlaceCombatant(combatant1, startzone);
        int numCombatants2 = BattleManager.Instance.battlefield.getCombatants(0, BattleManager.Instance.battlefield.numZones - 1).Count;

        Assert.IsTrue(numCombatants1 == numCombatants2 - 1);
    }

    [Test]
    public void TestResizeBattlefield()
    {
        int width1 = BattleManager.Instance.battlefield.Width;

        for (int i = 0; i < 10; i++)
        {
            Combatant combatant = GameObject.Instantiate(playerCombatantPrefab).GetComponent<Combatant>();
            BattleManager.Instance.battlefield.PlaceCombatant(combatant, 0);
        }

        int width2 = BattleManager.Instance.battlefield.Width;

        Assert.IsTrue(width2 > width1);
    }

    [Test]
    public void TestTargetAllActive()
    {
        Assert.IsTrue(false);
    }

    [Test]
    public void TestTargetAllActiveExceptSelf()
    {
        Assert.IsTrue(false);
    }

    [Test]
    public void TestTargetAllZones()
    {
        Assert.IsTrue(false);
    }

    [Test]
    public void TestTargetAllZonesExceptSelf()
    {
        Assert.IsTrue(false);
    }

    [Test]
    public void TestTargetInactive()
    {
        UIManager.CommandBuilder.Clear();
        IEquipableSkill resurrectSkill = GameObject.Instantiate(ResurrectSkillPrefab).GetComponent<IEquipableSkill>();
        UIManager.CommandBuilder.SetCommand(resurrectSkill.Command());

        List<Combatant> combatants = BattleManager.Instance.CombatantsInTurnOrderCopy;
        Combatant combatantAlive = combatants[0];
        Combatant combatantDead = combatants[1];
        combatantDead.name = "Deadboy";
        BattleManager.Instance.battlefield.PlaceCombatant(combatantAlive, 0);
        BattleManager.Instance.battlefield.PlaceCombatant(combatantDead, 0);

        BattleManager.Instance.SetDefeated(combatantDead);
        UIManager.CommandBuilder.SetActor(combatantAlive);
        UIManager.CommandBuilder.SetTarget(combatantDead);

        List<ITargetable> targets = BattleManager.Instance.battlefield.GetPotentialTargets(UIManager.CommandBuilder.GetFinishedCommand());

        Assert.AreEqual(combatantDead, targets[0]);
    }

    [Test]
    public void TestTargetSelfOnly()
    {
        Combatant combatant = BattleManager.Instance.CurrentCombatant;
        UIManager.CommandBuilder.Clear();
        UIManager.CommandBuilder.SetCommand(combatant.defendCommand);
        UIManager.CommandBuilder.SetActor(combatant);
        UIManager.CommandBuilder.SetTarget(combatant);

        List<ITargetable> targets = BattleManager.Instance.battlefield.GetPotentialTargets(UIManager.CommandBuilder.GetFinishedCommand());

        Assert.AreEqual(targets[0], combatant);
    }
}