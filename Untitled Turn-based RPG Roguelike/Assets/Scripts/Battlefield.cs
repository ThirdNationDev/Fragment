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
    private int initNumZones;

    public int numZones
    {
        get
        {
            return battlezones.Length;
        }

        private set { }
    }

    public Battlezone getZone(int i)
    {
        Assert.IsTrue(i >= 0);
        Assert.IsTrue(i < battlezones.Length);

        if ((0 <= i) && (i < battlezones.Length))
        {
            return battlezones[i];
        }
        else
        {
            return null;
        }
    }

    public Battlezone[] getZones(int start, int end)
    {
        if ((0 <= start) && (end < battlezones.Length) && (start <= end))
        {
            return battlezones.Skip(start).Take(end - start + 1).ToArray<Battlezone>();
        }
        else
        {
            return null;
        }
    }

    public void PlaceCombatants(List<Combatant> combatants)
    {
        Combatant c;
        int startingZone = 0;
        for (int i = 0; i < combatants.Count; i++)
        {
            c = combatants[i];
            if (c is PlayerCombatant)
            {
                startingZone = BattleManager.Instance.playerStartingZone;
            }
            else if (c is EnemyCombatant)
            {
                startingZone = BattleManager.Instance.enemyStartingZone;
            }
            battlezones[startingZone].AddCombatant(c);
        }
    }

    public void ResizeBattlefield()
    {
        //find the max tile width across all the zones
        int maxTileWidth = 0;
        int minZoneWidth;
        foreach (Battlezone zone in battlezones)
        {
            minZoneWidth = zone.combatants.Count + 2;
            maxTileWidth = minZoneWidth > maxTileWidth ? minZoneWidth : maxTileWidth;
        }

        foreach (Battlezone zone in battlezones)
        {
            zone.Resize(maxTileWidth);
        }
    }

    internal void CreateBattlefield()
    {
        if (BattleManager.Instance.playerStartingZone < 0
            || BattleManager.Instance.enemyStartingZone < 0
            || BattleManager.Instance.playerStartingZone >= initNumZones
            || BattleManager.Instance.enemyStartingZone >= initNumZones)
        {
            Debug.LogError("Battlefield starting zone is incorrect.");
        }

        int zoneTileNumber = battleZonePrefab.minTileNum;

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

    internal List<Combatant> getCombatants(int z1, int z2)
    {
        Battlezone[] zonesInRange = getZones(z1, z2);
        List<Combatant> targets = new List<Combatant>();
        foreach (Battlezone zone in zonesInRange)
        {
            targets.AddRange(zone.combatants);
        }
        return targets;
    }

    internal List<ITargetable> GetPotentialTargets(CommandManager.ICommand command)
    {
        List<ITargetable> targets = new();

        switch (command.CommandTargets)
        {
            case CommandManager.TargetType.TargetSelfOnly:
                targets.Add(command.Actor);

                break;

            case CommandManager.TargetType.TargetAllCombatants:
                targets = GetCombatantTargetsInRange(command.Actor.battlezone.zoneNumber, command.Skill.SkillStats.Range);
                break;

            case CommandManager.TargetType.TargetAllCombatantsExceptSelf:
                targets = GetCombatantTargetsInRange(command.Actor.battlezone.zoneNumber, command.Skill.SkillStats.Range);
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

    // Start is called before the first frame update
    private void Awake()
    {
        CreateBattlefield();
    }

    private List<ITargetable> GetAllZonesInRange(int startZoneNumber, int range)
    {
        int lowestZone = startZoneNumber - range;
        int highestZone = startZoneNumber + range;

        List<Battlezone> zones = getZones(lowestZone, highestZone).ToList<Battlezone>();
        return zones.Cast<ITargetable>().ToList();
    }

    private List<ITargetable> GetCombatantTargetsInRange(int startZoneNumber, int range)
    {
        int lowestZone = startZoneNumber - range;
        int highestZone = startZoneNumber + range;

        List<Combatant> combatants = getCombatants(lowestZone, highestZone);
        return combatants.Cast<ITargetable>().ToList();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}