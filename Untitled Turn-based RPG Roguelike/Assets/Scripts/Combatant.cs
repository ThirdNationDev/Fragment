using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Combatant : MonoBehaviour, IComparable
{
    public Battlezone battlezone;
    public int countdownToTurn;
    public BasicDefendSkill defendSkill;
    public BasicAttackSkill heavySkill;
    public BasicAttackSkill lightSkill;
    public BasicAttackSkill midSkill;
    public BasicMoveSkill moveSkill;
    public ParticleSystem particles;
    public CombatantStats stats;
    public Battlezone[] zonesInMoveRange;
    private UnitProfile unit;

    public CommandManager.ICommand defendCommand
    {
        get
        {
            return defendSkill.Command();
        }
        private set { }
    }

    public CommandManager.ICommand heavySkillCommand
    {
        get
        {
            return heavySkill.Command();
        }
        private set { }
    }

    public CommandManager.ICommand lightSkillCommand
    {
        get
        {
            return lightSkill.Command();
        }
        private set { }
    }

    public CommandManager.ICommand midSkillCommand
    {
        get
        {
            return midSkill.Command();
        }
        private set { }
    }

    public CommandManager.ICommand moveCommand
    {
        get
        {
            return moveSkill.Command();
        }
        private set { }
    }

    public virtual void Awake()
    {
        Debug.Log("Awake called for " + this.ToString());
        stats.AP = stats.startingAP;
        stats.health = stats.maxHealth;
        stats.stepsRemaining = stats.maxRange;
        particles.Stop();
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
        //BattleCommand command = new DeathBCom();
        //command.Initialize(this);
        //UIManager.Instance.commandSelected = command;
        //UIManager.Instance.SendCommand();
    }

    public virtual void EndTurn()
    {
        particles.Stop();
    }

    public virtual void ReceiveDamage(float damage)
    {
        //TODO: Play damaged animation
        //stats.health -= (int)Math.Round(damage);
        //if (stats.health <= 0)
        //{
        //    Death();
        //}
    }

    public virtual void StartTurn()
    {
        int lowZoneNum = battlezone.zoneNumber - stats.maxRange;
        int highZoneNum = battlezone.zoneNumber + stats.maxRange;
        zonesInMoveRange = BattleManager.Instance.battlefield.getZones(lowZoneNum, highZoneNum);
        particles.Play();
    }

    public override string ToString()
    {
        return this.name;
    }

    internal bool CanMoveTo(Battlezone target)
    {
        foreach (Battlezone zone in zonesInMoveRange)
        {
            if (zone == target)
            {
                return true;
            }
        }
        return false;
    }
}