using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Services.Creatures;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.CreatureEffects
{
    public interface ICreatureEffectService : IProjectService
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

        public void ApplyEffect(string effectId, ICreatureController targetCreatureController)
        {
            _creatureEffectApplyService.ApplyEffect(effectId, targetCreatureController);
        }
    }
}