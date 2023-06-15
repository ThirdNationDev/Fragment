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

//Test class: CombatantTestSuite - Inprogress
public abstract class Combatant : MonoBehaviour, IComparable, ITargetable
{
    public Battlezone battlezone;
    public int countdownToTurn;
    public BasicDefendSkill defendSkill;
    public BasicAttackSkill heavySkill;
    public BasicAttackSkill lightSkill;
    public BasicAttackSkill midSkill;
    public BasicMoveSkill moveSkill;

    public Battlezone[] zonesInMoveRange;

    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    private CombatantStats stats;

    public CommandManager.ICommand defendCommand
    {
        get
        {
            Assert.IsNotNull(defendSkill);
            Assert.IsNotNull(defendSkill.Command());
            return defendSkill.Command();
        }
        private set { }
    }

    public CommandManager.ICommand heavySkillCommand
    {
        get
        {
            Assert.IsNotNull(heavySkill);
            Assert.IsNotNull(heavySkill.Command());
            return heavySkill.Command();
        }
        private set { }
    }

    public CommandManager.ICommand lightSkillCommand
    {
        get
        {
            Assert.IsNotNull(lightSkill);
            Assert.IsNotNull(lightSkill.Command());
            return lightSkill.Command();
        }
        private set { }
    }

    public CommandManager.ICommand midSkillCommand
    {
        get
        {
            Assert.IsNotNull(midSkill);
            Assert.IsNotNull(midSkill.Command());
            return midSkill.Command();
        }
        private set { }
    }

    public CommandManager.ICommand moveCommand
    {
        get
        {
            Assert.IsNotNull(moveSkill);
            Assert.IsNotNull(moveSkill.Command());
            return moveSkill.Command();
        }
        private set { }
    }

    public string Name { get => this.name; }

    public ParticleSystem Particles
    {
        get
        {
            Assert.IsNotNull(particles);
            return particles;
        }
    }

    public CombatantStats Stats
    {
        get
        {
            Assert.IsNotNull(stats);
            return stats;
        }
    }

    public virtual void Awake()
    {
        Initialize();
    }

    public int CompareTo(object obj)
    {
        Assert.IsNotNull(obj);
        Assert.IsTrue(obj is Combatant);

        Combatant otherCombatant = obj as Combatant;
        return this.countdownToTurn.CompareTo(otherCombatant.countdownToTurn);
    }

    public virtual void Death()
    {
        CommandManager.Instance.AddAndExecuteCommand(CommandBuilder.DeathCommand(this));
    }

    public virtual void EndTurn()
    {
        Assert.IsTrue(BattleManager.Instance.CurrentCombatant == this);
        Assert.IsNotNull(Particles);
        Particles.Stop();
        Particles.Clear();
    }

    public virtual void ReceiveDamage(float damage)
    {
        //TODO: Play damaged animation
        Stats.health -= (int)Math.Round(damage);
        if (Stats.health <= 0)
        {
            Death();
        }
    }

    public virtual void StartTurn()
    {
        Assert.IsNotNull(BattleManager.Instance.battlefield);
        Assert.IsTrue(Stats.maxRange >= 0);
        Assert.IsNotNull(battlezone);

        int lowZoneNum = battlezone.zoneNumber - Stats.maxRange;
        int highZoneNum = battlezone.zoneNumber + Stats.maxRange;
        zonesInMoveRange = BattleManager.Instance.battlefield.GetZones(lowZoneNum, highZoneNum);

        Assert.IsTrue(zonesInMoveRange.Length > 0);

        Particles.Play();

        Assert.IsTrue(Particles.isEmitting);
    }

    public override string ToString()
    {
        return this.name;
    }

    public bool TestCanMoveTo(Battlezone zone)
    {
        return CanMoveTo(zone);
    }

    public void TestWrapperInitialize()
    {
        Initialize();
    }

    internal bool CanMoveTo(Battlezone target)
    {
        Assert.IsNotNull(target);
        Assert.IsTrue(zonesInMoveRange.Length > 0);

        foreach (Battlezone zone in zonesInMoveRange)
        {
            Assert.IsNotNull(zone);
            if (zone == target)
            {
                return true;
            }
        }
        return false;
    }

    private void Initialize()
    {
        Stats.AP = Stats.startingAP;
        Stats.health = Stats.maxHealth;
        Particles.Stop();
    }
}