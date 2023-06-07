using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackSkill : MonoBehaviour, IEquipableSkill
{
    public CommandManager.ICommand Command()
    {
        throw new System.NotImplementedException();
    }

    public Damage DealDamage()
    {
        throw new System.NotImplementedException();
    }
}