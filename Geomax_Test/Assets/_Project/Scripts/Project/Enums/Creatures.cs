using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.Enums
{
    public enum CreatureType
    { 
        NONE = 0,
        PLAYER = 1,
    }

    public enum CreatureComponentType
    { 
        None = 0,
        Move = 1,
        StateMachineDebug = 2,
        AnimatorController = 3,
    }
}