using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Services.Creatures;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.Services.CreatureEffects
{
    public interface ICreatureEffectService : IProjectService, ILateStartable, IMonoLateUpdatable
    {
        void ApplyEffect(string effectId, ICreatureController targetCreatureController);
    }

    public class CreatureEffectService : ICreatureEffectService
    {
        [Inject] private ICreatureEffectRepository _creatureEffectRepository;
        [Inject] private ICreatureEffectApplyService _creatureEffectApplyService;
        [Inject] private ICreatureEffectInvokeService _creatureEffectInvokeService;

        public async Task<bool> Init()
        {
            await _creatureEffectRepository.Init();
            await _creatureEffectApplyService.Init();
            await _creatureEffectInvokeService.Init();
            return true;
        }

        public bool Flush()
        {
            _creatureEffectRepository.Flush();
            _creatureEffectApplyService.Flush();
            _creatureEffectInvokeService.Flush();
            return true;
        }

        public Task<bool> OnLateStart()
        {
            _creatureEffectRepository.OnLateUpdate(0);
            return Task.FromResult(true);
        }

        public void OnLateUpdate(float deltaTime)
        {
            _creatureEffectRepository.OnLateUpdate(deltaTime);
        }

        public void ApplyEffect(string effectId, ICreatureController targetCreatureController)
        {
            _creatureEffectApplyService.ApplyEffect(effectId, targetCreatureController);
        }
    }
}