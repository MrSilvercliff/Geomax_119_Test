using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Effects.CreatureEffects;
using _Project.Scripts.GameScene.ObjectPools;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.Timers;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.CreatureEffects
{
    public interface ICreatureEffectApplyService : IProjectService
    {
        void ApplyEffect(string effectId, ICreatureController targetCreatureController);
        void RemoveEffect(ICreatureEffectController creatureEffectController);
    }

    public class CreatureEffectApplyService : ICreatureEffectApplyService
    {
        [Inject] private IProjectBalanceService _projectBalanceService;
        [Inject] private ITimerService _timerService;
        [Inject] private IGameSceneObjectPoolService _gameSceneObjectPoolService;
        [Inject] private ICreatureEffectRepository _effectRepository;

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
            var effectBalanceStorage = _projectBalanceService.Effects;

            var tryResult = effectBalanceStorage.TryGetById(effectId, out var effectBalanceModel);

            if (!tryResult)
                return;

            var creatureControllerInstanceId = targetCreatureController.InstanceID;

            var pool = _gameSceneObjectPoolService.CreatureEffectControllerPool;
            var effectController = pool.Spawn();
            effectController.Setup(effectBalanceModel);
            _effectRepository.Add(creatureControllerInstanceId, effectController);

            if (effectBalanceModel.TriggerType == EffectTriggerType.TIME_TICK)
            {
                var timerId = _timerService.IdProvider.GetCreatureAbilityCooldownTimerId(creatureControllerInstanceId, effectId);
                ITimerTick timer = null;

                if (effectBalanceModel.DurationType == EffectDurationType.PERMANENT)
                    timer = _timerService.StartTimerCustomSecondTickInfinite(timerId, effectBalanceModel.TickTime);
                else
                    timer = _timerService.StartTimerCustomSecondTick(timerId, effectBalanceModel.DurationSeconds, effectBalanceModel.TickTime);

                effectController.Setup(timer);
            }
        }

        public void RemoveEffect(ICreatureEffectController creatureEffectController)
        {
            LogUtils.Error(this, $"TODO: REMOVE CREATURE EFFECT!");
        }
    }
}