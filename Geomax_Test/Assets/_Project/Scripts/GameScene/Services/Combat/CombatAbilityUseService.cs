using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Combat
{
    public interface ICombatAbilityUseService : IProjectService
    {
        IAbilityResult UseAbility(ICreatureController creatureFrom, IAbility ability);
    }

    public class CombatAbilityUseService : ICombatAbilityUseService
    {
        [Inject] private AbilityResult.Factory _abilityResultFactory;

        [Inject] private AbilityResultItemDamage.Factory _resultItemDamageFactory;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public IAbilityResult UseAbility(ICreatureController creatureFrom, IAbility ability)
        {
            IAbilityResult result = null;

            switch (ability.AbilityType)
            {
                case AbilityType.BASIC_ATTACK:
                    result = UseAbilityBasicAttack(creatureFrom, (IAbilityAttack)ability);
                    break;
            }

            return result;
        }

        private IAbilityResult UseAbilityBasicAttack(ICreatureController creatureFrom, IAbilityAttack ability)
        {
            var creatureTarget = creatureFrom.AttackTargetCreatureController;
            var damageItem = _resultItemDamageFactory.Create(creatureTarget, ability.Damage);

            var resultItems = new List<IAbilityResultItem>();
            resultItems.Add(damageItem);

            var result = _abilityResultFactory.Create(creatureFrom, resultItems);
            return result;
        }
    }
}