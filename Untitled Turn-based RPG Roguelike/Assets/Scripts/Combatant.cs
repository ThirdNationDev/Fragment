using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    private UnitProfile unit;
    public Battlezone battlezone;
    public CombatantStats stats;
    
    [SerializeField]
    public BasicDefendSkill moveSkill;
    [SerializeField]
    IEquipableSkill lightSkill;
    [SerializeField]
    IEquipableSkill midSkill;
    [SerializeField]
    IEquipableSkill heavySkill;
    [SerializeField]
    IEquipableSkill defendSkill;

    public BattleCommand defendCommand
    {
        get
        {
            return defendSkill.Command(this);
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

    public Battlezone[] zonesInMoveRange;



    public virtual void EnterCombat(Battlezone zone)
    {
        battlezone = zone;
        zone.AddCombatant(this);
        stats.AP = stats.startingAP;
        stats.health = stats.maxHealth;
        stats.stepsRemaining = stats.maxRange;


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
        int lowZoneNum = battlezone.zoneNumber - stats.maxRange;
        int highZoneNum = battlezone.zoneNumber + stats.maxRange;
        zonesInMoveRange = BattleManager.Instance.battlefield.getZones(lowZoneNum, highZoneNum);
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

    public override string ToString()
    {
        return this.name;
    }
}
