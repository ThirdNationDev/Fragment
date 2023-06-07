using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipableSkill
{
    public CommandManager.ICommand Command();

    public Damage DealDamage();
}