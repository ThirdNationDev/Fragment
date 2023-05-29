using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveSkill : MonoBehaviour, IEquipableSkill
{
    public BattleCommand Command()
    {
        return new ChangeZoneBCom();
    }

    public BattleCommand Command(Combatant actor)
    {
        BattleCommand command = new ChangeZoneBCom();
        command.Initialize(actor);
        return command;
    }

 
}
