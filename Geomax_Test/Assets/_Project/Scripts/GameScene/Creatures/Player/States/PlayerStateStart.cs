using _Project.Scripts.GameScene.Services.Combat;
using _Project.Scripts.GameScene.Services.Creatures;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Timers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Player.States
{
    public interface IPlayerStateStart : IPlayerStateControllerBase
    { 
    }

    public class PlayerStateStart : PlayerStateControllerBase<PlayerStateStart>, IPlayerStateStart
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Start;

        [Inject] private ITimerService _timerService;
        [Inject] private ICreatureService _creatureService;

        public PlayerStateStart(IPlayerController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
        }

        protected override void OnFlush()
        {
        }

        protected override void OnEnter()
        {
            StartBasicAttackCooldownTimer();
            SetupAttackTarget();
            _creatureStateMachine.EnterState(StateMachineStateType.CreatureState_Idle);
        }

        public override void OnFixedUpdate(float deltaTime)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
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

        private void StartBasicAttackCooldownTimer()
        {
            var basicAttackAbility = _creatureModel.GetBasicAttackAbility();
            var abilityId = basicAttackAbility.Id;
            var creatureControllerInstanceId = _creatureController.InstanceID;
            var timerId = _timerService.IdProvider.GetCreatureAbilityCooldownTimerId(creatureControllerInstanceId, abilityId);
            _timerService.StartTimer(timerId, basicAttackAbility.CooldownSeconds);
        }

        private void SetupAttackTarget()
        { 
            var attackTarget = _creatureService.GetAttackTargetForPlayerCreature();
            _creatureController.SetAttackTarget(attackTarget);
        }
    }
}