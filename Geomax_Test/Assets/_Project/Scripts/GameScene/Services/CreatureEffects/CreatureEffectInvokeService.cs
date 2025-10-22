using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Effects.CreatureEffects;
using _Project.Scripts.GameScene.Services.Creatures;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.CreatureEffects
{
    public interface ICreatureEffectInvokeService : IProjectService
    {
        void InvokeCreatureEffect(ICreatureEffectController creatureEffectController);
    }

    public class CreatureEffectInvokeService : ICreatureEffectInvokeService
    {
        [Inject] private ICreatureEffectRepository _creatureEffectRepository;
        [Inject] private ICreatureService _creatureService;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void InvokeCreatureEffect(ICreatureEffectController creatureEffectController)
        {
            var tryResult = _creatureEffectRepository.TryGetCreatureControllerInstanceId(creatureEffectController, out var creatureControllerInstanceId);

            if (!tryResult)
            {
                LogUtils.Error(this, $"CREATURE CONTROLLER INSTANCE ID WITH EFFECT [{creatureEffectController.EffectId}] DOES NOT EXIST!");
                return;
            }

            tryResult = _creatureService.TryGetCreatureControllerByInstanceId(creatureControllerInstanceId, out var creatureController);

            if (!tryResult)
            {
                LogUtils.Error(this, $"CREATURE CONTROLLER WITH INSTANCE ID [{creatureControllerInstanceId}] DOES NOT EXIST!");
                return;
            }
        }

        private void InvokeCreatureEffectProcess(ICreatureEffectController creatureEffectController, ICreatureController creatureController)
        { 

        }
    }
}