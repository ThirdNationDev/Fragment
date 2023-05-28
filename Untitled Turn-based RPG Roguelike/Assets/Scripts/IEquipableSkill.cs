using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipableSkill
{ 
    public BattleCommand Command();

    public BattleCommand Command(Combatant actor);
}
