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
using System.Linq;

public class Battlefield : MonoBehaviour
{
    [SerializeField]
    private Battlezone battleZonePrefab;

    private Battlezone[] battlezones;

    [SerializeField]
    private int initNumZones = 10;

    public int numZones
    {
        get
        {
            Assert.IsNotNull(battlezones);
            return battlezones.Length;
        }
    }

    public int Width { get => FindMaxTileWidth(); }

    public void CreateBattlefield()
    {
        Assert.IsTrue(initNumZones > 0);
        Assert.IsFalse(BattleManager.Instance.playerStartingZone < 0);
        Assert.IsFalse(BattleManager.Instance.enemyStartingZone < 0);
        Assert.IsFalse(BattleManager.Instance.playerStartingZone >= initNumZones);
        Assert.IsFalse(BattleManager.Instance.enemyStartingZone >= initNumZones);
        Assert.IsTrue(battleZonePrefab.minTileNum > 0);

        battlezones = new Battlezone[initNumZones];
        for (int i = 0; i < initNumZones; i++)
        {
            Battlezone zone = Instantiate(battleZonePrefab);
            float offset = i * zone.length;
            zone.transform.parent = this.gameObject.transform;
            zone.transform.position += new Vector3(0, 0, offset);
            zone.name = "Battlezone " + i.ToString();
            zone.zoneNumber = i;
            zone.battlefield = this;
            battlezones[i] = zone;
        }
    }

    public List<Combatant> getCombatants(int z1, int z2)
    {
        Battlezone[] zonesInRange = GetZones(z1, z2);
        List<Combatant> targets = new List<Combatant>();
        foreach (Battlezone zone in zonesInRange)
        {
            targets.AddRange(zone.combatants);
        }
        return targets;
    }

    public List<ITargetable> GetPotentialTargets(CommandManager.ICommand command)
    {
        Assert.IsNotNull(command);
        Assert.IsNotNull(command.Actor);
        Assert.IsNotNull(command.Skill);
        Assert.IsNotNull(command.Actor.battlezone);

        List<ITargetable> targets = new();

        switch (command.CommandTargets)
        {
            case CommandManager.TargetType.TargetSelfOnly:
                targets.Add(command.Actor);

                break;

            case CommandManager.TargetType.TargetInactiveCombatants:
                targets = GetInactiveCombatantTargetsInRange(command.Actor.battlezone.zoneNumber, command.Skill.SkillStats.Range);
                break;

            case CommandManager.TargetType.TargetAllActiveCombatants:
                targets = GetActiveCombatantTargetsInRange(command.Actor.battlezone.zoneNumber, command.Skill.SkillStats.Range);
                break;

            case CommandManager.TargetType.TargetAllActiveCombatantsExceptSelf:
                targets = GetActiveCombatantTargetsInRange(command.Actor.battlezone.zoneNumber, command.Skill.SkillStats.Range);
                Assert.IsTrue(targets.Remove(command.Actor));
                break;

            case CommandManager.TargetType.TargetAllZones:
                targets = GetAllZonesInRange(command.Actor.battlezone.zoneNumber, command.Skill.SkillStats.Range);
                break;

            case CommandManager.TargetType.TargetAllZonesExceptCurrent:
                targets = GetAllZonesInRange(command.Actor.battlezone.zoneNumber, command.Skill.SkillStats.Range);
                Assert.IsTrue(targets.Remove(command.Actor.battlezone));
                break;

            default:
                Debug.LogError("Invalid TargetType code used in GetPotentialTargets by " + command.ToString());
                break;
        }
        return targets;
    }

    public Battlezone GetZone(int i)
    {
        Assert.IsTrue(i >= 0);
        Assert.IsTrue(i < battlezones.Length);

        return battlezones[i];
    }

    public Battlezone[] GetZones(int start, int end)
    {
        Assert.IsTrue(start <= end);

        start = Math.Clamp(start, 0, battlezones.Length - 1);
        end = Math.Clamp(end, 0, battlezones.Length - 1);

        return battlezones.Skip(start).Take(end - start + 1).ToArray<Battlezone>();
    }

    public void ResizeBattlefield()
    {
        int maxTileWidth = FindMaxTileWidth();
        foreach (Battlezone zone in battlezones)
        {
            zone.Resize(maxTileWidth);
        }
    }

    public void PlaceCombatant(Combatant combatant, int zoneNumber)
    {
        PlaceCombatant(combatant, GetZone(zoneNumber));
    }

    public void PlaceCombatant(Combatant combatant, Battlezone zone)
    {
        Assert.IsNotNull(combatant);
        Assert.IsNotNull(zone);
        Assert.IsTrue(battlezones.Contains(zone));

        zone.AddCombatant(combatant);
        ResizeBattlefield();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        CreateBattlefield();
    }

    private int FindMaxTileWidth()
    {
        int maxTileWidth = 0;
        int minZoneWidth;
        foreach (Battlezone zone in battlezones)
        {
            minZoneWidth = zone.combatants.Count + 2;
            maxTileWidth = minZoneWidth > maxTileWidth ? minZoneWidth : maxTileWidth;
        }

        return maxTileWidth;
    }

    private List<ITargetable> GetActiveCombatantTargetsInRange(int zoneNumber, int range)
    {
        List<ITargetable> targets = GetCombatantTargetsInRange(zoneNumber, range);
        foreach (ITargetable inactiveTarget in BattleManager.Instance.DefeatedCombatantsCopy)
        {
            targets.Remove(inactiveTarget);
        }
        return targets;
    }

    private List<ITargetable> GetAllZonesInRange(int startZoneNumber, int range)
    {
        int lowestZone = startZoneNumber - range;
        int highestZone = startZoneNumber + range;

        List<Battlezone> zones = GetZones(lowestZone, highestZone).ToList<Battlezone>();
        return zones.Cast<ITargetable>().ToList();
    }

    private List<ITargetable> GetCombatantTargetsInRange(int startZoneNumber, int range)
    {
        int lowestZone = startZoneNumber - range;
        int highestZone = startZoneNumber + range;

        List<Combatant> combatants = getCombatants(lowestZone, highestZone);
        return combatants.Cast<ITargetable>().ToList();
    }

    private List<ITargetable> GetInactiveCombatantTargetsInRange(int zoneNumber, int range)
    {
        List<ITargetable> targets = GetCombatantTargetsInRange(zoneNumber, range);
        foreach (ITargetable activeTarget in BattleManager.Instance.CombatantsInTurnOrderCopy)
        {
            targets.Remove(activeTarget);
        }
        return targets;
    }
}