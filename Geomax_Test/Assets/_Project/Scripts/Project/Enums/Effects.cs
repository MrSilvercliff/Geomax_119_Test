using UnityEngine;

namespace _Project.Scripts.Project.Enums
{
    public enum EffectTriggerType
    {
        NONE = 0,
        DAMAGE_DEAL = 1,
        DAMAGE_RECEIVE = 2,
        SKILL_USE = 3,
        TIME_TICK = 4,
    }

    public enum EffectDurationType
    { 
        NONE = 0,
        PERMANENT = 1,
        TEMPORARY = 2,
    }

    public enum EffectTargetType
    { 
        NONE = 0,
        SELF = 1,
        ATTACK_TARGET = 2,
        ALL_OTHER_SIDE = 3,
        ALL = 4,
    }

    public enum EffectType
    { 
        NONE = 0,
        MANA_POINTS_REGEN = 1,
        EFFECT_TRIGGER = 2,
        STUN = 3,
        HEAL_BY_MAX_HIT_POINTS = 4,
        DAMAGE_MULTIPLY = 5,
        BASIC_ATTACK_COOLDOWN_MULTIPLY = 6,
    }

    public enum EffectValueType
    { 
        NONE = 0,
        PERCENTAGE = 1,
        SOLID = 2,
    }
}