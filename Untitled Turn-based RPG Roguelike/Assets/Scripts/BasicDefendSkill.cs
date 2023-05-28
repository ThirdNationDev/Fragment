using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDefendSkill : MonoBehaviour, IEquipableSkill
{
    public BattleCommand Command()
    {
        return new DefendBCom();
    }

    public BattleCommand Command(Combatant actor)
    {
        DefendBCom command = new DefendBCom();
        command.Initialize(actor);
        return command;
    }
}
