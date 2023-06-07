using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDefendSkill : MonoBehaviour, IEquipableSkill
{
    public CommandManager.ICommand Command()
    {
        return new DefendCommand();
    }

    public Damage DealDamage()
    {
        throw new System.NotImplementedException();
    }
}