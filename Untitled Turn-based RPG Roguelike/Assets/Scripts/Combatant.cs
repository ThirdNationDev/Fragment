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

    public BattleCommand defendCommand
    {
        get
        {
            return defendSkill.Command(this);
        }
        private set { }
    }

    public BattleCommand heavySkillCommand
    {
        get
        {
            return heavySkill.Command(this);
        }
        private set { }
    }

    public BattleCommand lightSkillCommand
    {
        get
        {
            return lightSkill.Command(this);
        }
        private set { }
    }

    public BattleCommand midSkillCommand
    {
        get
        {
            return midSkill.Command(this);
        }
        private set { }
    }

    public BattleCommand moveCommand
    {
        get
        {
            return moveSkill.Command(this);
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

    public virtual void MoveBackOne()
    {
        Battlezone targetZone = BattleManager.Instance.battlefield.getZone(battlezone.zoneNumber - 1);
        //ChangeZoneBCom command = new ChangeZoneBCom(this, targetZone);
        // BattleManager.Instance.ExecuteCommand(command);
    }

    public virtual void MoveForwardOne()
    {
        Battlezone targetZone = BattleManager.Instance.battlefield.getZone(battlezone.zoneNumber + 1);
        //ChangeZoneBCom command = new ChangeZoneBCom(this, targetZone);
        //BattleManager.Instance.ExecuteCommand(command);
        //MoveTo(targetZone);
    }

    public virtual void MoveTo(Battlezone newZone)
    {
        int distance = Mathf.Abs(battlezone.zoneNumber - newZone.zoneNumber);
        //Move combatant to new zone and locaiton
        if (distance <= stats.stepsRemaining)
        {
            battlezone.RemoveCombatant(this);
            newZone.AddCombatant(this);
            battlezone = newZone;
            stats.AP--;
        }
    }

    public virtual void ReceiveDamage(float damage)
    {
        //TODO: Play damaged animation
        stats.health -= (int)Math.Round(damage);
        if (stats.health <= 0)
        {
            Death();
        }
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
}