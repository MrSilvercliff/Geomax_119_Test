using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.Enums
{
    public enum AbilityType
    { 
        NONE = 0,
        BASIC_ATTACK = 1,
        SKILL_ATTACK = 2,
        SKILL_BUFF = 3,
    }

    public enum AbilityResultItemType
    { 
        NONE = 0,
        DamageDeal = 1,
        ApplyEffect = 2,
    }
}