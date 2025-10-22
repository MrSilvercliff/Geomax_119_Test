using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Services.Balance;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.CreatureEffects
{
    public interface ICreatureEffectApplyService : IProjectService
    {
        void ApplyEffect(string effectId, ICreatureController targetCreatureController);
    }

    public class CreatureEffectApplyService : ICreatureEffectApplyService
    {
        [Inject] private IProjectBalanceService _projectBalanceService;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void ApplyEffect(string effectId, ICreatureController targetCreatureController)
        {
        }
    }
}