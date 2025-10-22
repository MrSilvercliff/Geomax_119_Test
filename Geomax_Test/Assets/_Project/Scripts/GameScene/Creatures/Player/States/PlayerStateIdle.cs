using _Project.Scripts.GameScene.Creatures.Basis.Components;
using _Project.Scripts.Project.Animations;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Timers;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Player.States
{
    public interface IPlayerStateIdle : IPlayerStateControllerBase
    { 
    }

    public class PlayerStateIdle : PlayerStateControllerBase<PlayerStateIdle>, IPlayerStateIdle
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Idle;

        [Inject] private ITimerService _timerService;

        private ICreatureComponentAnimator _componentAnimator;
        private ITimer _basicAttackCooldownTimer;
        private bool _canAttack;

        public PlayerStateIdle(IPlayerController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
            _componentAnimator = _creatureComponentContainer.GetComponent<CreatureComponentAnimator>(CreatureComponentType.AnimatorController);
        }

        protected override void OnFlush()
        {
            _componentAnimator = null;

            if (_basicAttackCooldownTimer != null)
            {
                _basicAttackCooldownTimer.ExpiredEvent -= OnBasicAttackCooldownExpired;
                _basicAttackCooldownTimer = null;
            }
        }

        protected override void OnEnter()
        {
            _canAttack = false;
            GetBasicAttackCooldownTimer();
            _componentAnimator.AnimatorController.Play(AnimatorStateHash.Idle);
        }

        public override void OnFixedUpdate(float deltaTime)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!_canAttack)
                return;

            _creatureController.CheckAttackTarget();

            if (_creatureController.AttackTargetCreatureController == null)
                return;

            _canAttack = false;
            _creatureStateMachine.EnterState(StateMachineStateType.CreatureState_Attack);
        }

        public override void OnLateUpdate(float deltaTime)
        {
        }

        protected override void OnExit()
        {
        }

        public override void OnAnimationEvent(CreatureAnimationEvent creatureAnimationEvent)
        {
        }

        private void GetBasicAttackCooldownTimer()
        {
            var basicAttackAbility = _creatureModel.GetBasicAttackAbility();
            var abilityId = basicAttackAbility.Id;
            var creatureControllerInstanceId = _creatureController.InstanceID;
            var timerId = _timerService.IdProvider.GetCreatureAbilityCooldownTimerId(creatureControllerInstanceId, abilityId);
            var tryGet = _timerService.TryGetTimer(timerId, out var timer);

            if (!tryGet)
                return;

            if (timer.Expired)
            {
                _canAttack = true;
                return;
            }

            _basicAttackCooldownTimer = timer;
            _basicAttackCooldownTimer.ExpiredEvent += OnBasicAttackCooldownExpired;
        }

        private void OnBasicAttackCooldownExpired(ITimer timer)
        {
            _basicAttackCooldownTimer.ExpiredEvent -= OnBasicAttackCooldownExpired;
            _canAttack = true;
        }
    }
}