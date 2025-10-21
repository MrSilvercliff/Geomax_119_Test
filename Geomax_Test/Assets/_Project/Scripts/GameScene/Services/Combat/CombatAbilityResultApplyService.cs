using _Project.Scripts.GameScene.Abilities;
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
        }
    }
}