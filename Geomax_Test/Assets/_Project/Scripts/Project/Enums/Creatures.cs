using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.Enums
{
    public enum CreatureType
    { 
        NONE = 0,
        PLAYER = 1,
        ENEMY = 2,
    }

    public enum CreatureResourceType
    { 
        NONE = 0,
        HIT_POINTS = 1,
        MANA_POINTS = 2,
    }

    public enum CreatureComponentType
    { 
        None = 0,
        Move = 1,
        StateMachineDebug = 2,
        AnimatorController = 3,
    }

    public enum CreatureAnimationEvent
    { 
        NONE,
        Animation_Finished,
        Attack_Deal_Damage,
        Death_Prefab_Set_Active_False,
    }
}