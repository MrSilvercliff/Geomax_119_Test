using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using UnityEngine;

namespace _Project.Scripts.GameScene.Abilities
{
    public interface IAbilityResultItem
    { 
        AbilityResultItemType ItemType { get; }
        ICreatureController CreatureTarget { get; }

        void Flush();
    }

    public abstract class AbilityResultItem : IAbilityResultItem
    {
        public abstract AbilityResultItemType ItemType { get; }
        public ICreatureController CreatureTarget { get; protected set; }

        public void Flush()
        {
            CreatureTarget = null;
            OnFlush();
        }

        protected abstract void OnFlush();
    }
}