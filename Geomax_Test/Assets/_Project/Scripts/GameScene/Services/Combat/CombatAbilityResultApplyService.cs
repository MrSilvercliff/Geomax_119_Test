using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Combat
{
    public interface ICombatAbilityResultApplyService : IProjectService
    {
        void ApplyAbilityResult(IAbilityResult abilityResult);
    }

    public class CombatAbilityResultApplyService : ICombatAbilityResultApplyService
    {
        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void ApplyAbilityResult(IAbilityResult abilityResult)
        {
            var creatureFrom = abilityResult.CreatureFrom;
            var abilityResultItems = abilityResult.ResultItems;

            foreach (var resultItem in abilityResultItems)
                ApplyAbilityResultItem(creatureFrom, resultItem);
        }

        private void ApplyAbilityResultItem(ICreatureController creatureFrom, IAbilityResultItem abilityResultItem)
        { 
            switch (abilityResultItem.ItemType)
            {
                case AbilityResultItemType.DamageDeal:
                    ApplyDamage((IAbilityResultItemDamage)abilityResultItem);
                    break;
            }
        }

        private void ApplyDamage(IAbilityResultItemDamage damageItem)
        {
            var creature = damageItem.CreatureTarget;

            if (!creature.IsAlive())
                return;

            var creatureHitPoints = creature.CreatureModel.GetResourceValue(CreatureResourceType.HIT_POINTS);
            var newValue = creatureHitPoints.CurrentValue - damageItem.Damage;
            creatureHitPoints.SetCurrentValue(newValue);

            if (creatureHitPoints.CurrentValue > creatureHitPoints.MinValue)
                creature.OnHit();
            else
                creature.OnDeath();
        }
    }
}