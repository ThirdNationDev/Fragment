using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    private UnitProfile unit;
    public Battlezone battlezone;
    public CombatantStats stats;
    
    public BattleCommand movementCommand;
    public BattleCommand lightAttack;
    public BattleCommand midAttack;
    public BattleCommand heavyAttack;
    public BattleCommand defendBCom;



    public virtual void EnterCombat(Battlezone zone)
    {
        battlezone = zone;
        zone.AddCombatant(this);
        stats.AP = stats.startingAP;
        stats.health = stats.maxHealth;
        stats.stepsRemaining = stats.maxRange;

        //defendCommand = ScriptableObject.CreateInstance("DefendBCom") as BattleCommand;
        defendBCom = ScriptableObject.CreateInstance<DefendBCom>();
        defendBCom.Initialize(this);
    }

    public virtual void MoveTo(Battlezone newZone)
    {
        int distance = Mathf.Abs(battlezone.zoneNumber - newZone.zoneNumber);
        //Move combatant to new zone and locaiton
        if (distance <= stats.stepsRemaining)
        {
            battlezone.RemoveCombatant(this.gameObject);
            newZone.AddCombatant(this);
            battlezone = newZone;
            stats.AP--;
        }

    }

    public virtual void NewTurn()
    {
        stats.stepsRemaining = stats.maxRange;
    }

    public virtual void MoveForwardOne()
    {
        Battlezone targetZone = BattleManager.Instance.battlefield.getZone(battlezone.zoneNumber + 1);
        //ChangeZoneBCom command = new ChangeZoneBCom(this, targetZone);
        //BattleManager.Instance.ExecuteCommand(command);
        //MoveTo(targetZone);
    }

    public virtual void MoveBackOne()
    {
        Battlezone targetZone = BattleManager.Instance.battlefield.getZone(battlezone.zoneNumber - 1);
        //ChangeZoneBCom command = new ChangeZoneBCom(this, targetZone);
       // BattleManager.Instance.ExecuteCommand(command);
    }
}
