using UnityEngine;

namespace _Project.Scripts.Project.Enums
{
    public enum EffectTriggerType
    {
        NONE = 0,
        Attack = 1,
        Hit = 2,
        SkillUse = 3,
        TimerTick = 4,
    }

    public enum EffectDuration
    { 
        NONE = 0,
        Permanent = 1,
        Temporary = 2,
    }

    public enum EffectTargetType
    { 
        NONE = 0,
        Self = 1,
        AttackTarget = 2,
        AllOtherSide = 3,
        All = 4,
    }

    public enum EffectType
    { 
        NONE = 0,
        Stun = 1,
        Heal = 2,
        DamageMultiply = 3,
        BasicAttackCooldownMultiply = 4,
    }
}