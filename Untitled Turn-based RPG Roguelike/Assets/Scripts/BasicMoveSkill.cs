using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveSkill : MonoBehaviour, IEquipableSkill
{
    public CommandManager.ICommand Command()
    {
        return new MoveCommand();
    }

    public Damage DealDamage()
    {
        throw new System.NotImplementedException();
    }
}