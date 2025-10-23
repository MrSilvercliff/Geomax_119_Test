using _Project.Scripts.GameScene.Services.CreatureEffects;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance.Models;
using _Project.Scripts.Project.Services.Timers;
using System;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync;

namespace _Project.Scripts.GameScene.Effects.CreatureEffects
{
    public interface ICreatureEffectController : IFlushable
    {
        string EffectId { get; }
        string TimerId { get; }

        EffectTargetType TargetType { get; }
        EffectTriggerType TriggerType { get; }
        EffectType EffectType { get; }
        EffectValueType EffectValueType { get; }

        float Parameter { get; }
        float DurationSeconds { get; }

        bool Visible { get; }

        void Setup(IEffectBalanceModel effectBalanceModel);
        void Setup(ITimerTick timer);
    }

    public class CreatureEffectController : ICreatureEffectController
    {
        public string EffectId => _balanceModel.Id;
        public string TimerId 
        { 
            get 
            {
                if (_timer == null)
                    return string.Empty;

                return _timer.Id;
            } 
        }

        public EffectTargetType TargetType => _balanceModel.TargetType;
        public EffectTriggerType TriggerType => _balanceModel.TriggerType;
        public EffectType EffectType => _balanceModel.EffectType;
        public EffectValueType EffectValueType => _balanceModel.EffectValueType;

        public float Parameter => _balanceModel.Parameter;
        public float DurationSeconds => _balanceModel.DurationSeconds;

        public bool Visible => _balanceModel.Visible;

        [Inject] private ICreatureEffectApplyService _effectApplyService;
        [Inject] private ICreatureEffectInvokeService _effectInvokeService;

        private IEffectBalanceModel _balanceModel;
        private ITimerTick _timer;

        public CreatureEffectController() 
        {
        }

        public void Setup(IEffectBalanceModel effectBalanceModel)
        {
            _balanceModel = effectBalanceModel;
        }

        public void Setup(ITimerTick timer)
        {
            _timer = timer;
            _timer.TickEvent += OnTimerTick;
            _timer.ExpiredEvent += OnTimerExpired;
        }

        public bool Flush()
        {
            if (_timer != null)
            { 
                _timer.TickEvent -= OnTimerTick;
                _timer.ExpiredEvent -= OnTimerExpired;
                _timer = null;
            }

            return true;
        }

        private void OnTimerTick(ITimerTick tick)
        {
            _effectInvokeService.InvokeCreatureEffect(this);
        }

        private void OnTimerExpired(ITimer timer)
        {
            _effectApplyService.RemoveEffect(this);
        }

        public class Pool : MemoryPool<CreatureEffectController> 
        {
            protected override void OnDespawned(CreatureEffectController item)
            {
                base.OnDespawned(item);
                item.Flush();
            }
        }
    }
}