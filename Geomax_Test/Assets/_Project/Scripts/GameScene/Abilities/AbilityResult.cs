using _Project.Scripts.GameScene.Creatures.Basis;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Abilities
{
    public interface IAbilityResult
    { 
        ICreatureController CreatureFrom { get; }
        IReadOnlyList<IAbilityResultItem> ResultItems { get; }

        void Flush();
    }

    public class AbilityResult : IAbilityResult
    {
        public ICreatureController CreatureFrom { get; private set; }
        public IReadOnlyList<IAbilityResultItem> ResultItems { get; private set; }

        public AbilityResult(ICreatureController creatureFrom, IReadOnlyList<IAbilityResultItem> resultItems)
        { 
            CreatureFrom = creatureFrom;
            ResultItems = resultItems;
        }

        public void Flush()
        {
            foreach (var item in ResultItems)
                item.Flush();

            CreatureFrom = null;
            ResultItems = null;
        }

        public class Factory : PlaceholderFactory<ICreatureController, IReadOnlyList<IAbilityResultItem>, AbilityResult> { }
    }
}