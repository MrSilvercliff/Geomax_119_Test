using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.Enums
{
    public enum StateMachineType
    { 
        NONE = 0,

        MAIN_SCENE_NULL = 1000,

        GAME_SCENE_NULL = 2000,
        CREATURE_PLAYER = 2001,
        CREATURE_ENEMY = 2002,
    }

    public enum StateMachineStateType
    {
        NONE = 0,
        
        CreatureState_Start = 1,
        CreatureState_Idle = 2,
        CreatureState_Attack = 3,
        CreatureState_Hit = 4,
        CreatureState_Death = 5,
        CreatureState_Despawn = 6,
    }
}