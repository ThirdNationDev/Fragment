using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackSkill : MonoBehaviour, IEquipableSkill
{
    public BattleCommand Command()
    {
        return new AttackBCom();
    }

    public BattleCommand Command(Combatant actor)
    {
        BattleCommand command = new AttackBCom();
        command.Initialize(actor);
        return command;

    }

}
