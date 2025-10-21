using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Abilities
{
    public interface IAbilityResultItemDamage : IAbilityResultItem
    { 
        int Damage { get; }
    }

    public class AbilityResultItemDamage : AbilityResultItem, IAbilityResultItemDamage
    {
        public override AbilityResultItemType ItemType => AbilityResultItemType.DamageDeal;

        public int Damage { get; private set; }

        public AbilityResultItemDamage(ICreatureController creatureTarget, int damage)
        { 
            CreatureTarget = creatureTarget;
            Damage = damage;
        }

        protected override void OnFlush()
        {
        }

        public class Factory : PlaceholderFactory<ICreatureController, int, AbilityResultItemDamage> { }
    }
}