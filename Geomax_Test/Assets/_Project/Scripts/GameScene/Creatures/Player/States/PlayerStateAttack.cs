using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.GameScene.Creatures.Basis.Components;
using _Project.Scripts.Project.Animations;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Timers;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Creatures.Player.States
{
    public interface IPlayerStateAttack : IPlayerStateControllerBase
    { 
    }

    public class PlayerStateAttack : PlayerStateControllerBase<PlayerStateAttack>, IPlayerStateAttack
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Attack;

        [Inject] private ITimerService _timerService;

        private ICreatureComponentAnimator _componentAnimator;
        private IAbilityAttack _abilityAttack;

        public PlayerStateAttack(IPlayerController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
            _componentAnimator = _creatureComponentContainer.GetComponent<ICreatureComponentAnimator>(CreatureComponentType.AnimatorController);
            _abilityAttack = _creatureModel.GetBasicAttackAbility();
        }

        protected override void OnEnter()
        {
            _componentAnimator.AnimatorController.Play(AnimatorStateHash.Attack);
            StartBasicAttackCooldownTimer();
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
            switch (creatureAnimationEvent) 
            {
                case CreatureAnimationEvent.Attack_Deal_Damage:
                    LogUtils.Error(this, $"Creature [{_creatureController.Transform.gameObject.name}] attack deal damage!");
                    break;

                case CreatureAnimationEvent.Animation_Finished:
                    _creatureStateMachine.EnterState(StateMachineStateType.CreatureState_Idle);
                    break;
            }
        }

        private void StartBasicAttackCooldownTimer()
        {
            var basicAttackAbility = _creatureModel.GetBasicAttackAbility();
            var abilityId = basicAttackAbility.Id;
            var creatureControllerInstanceId = _creatureController.InstanceID;
            var timerId = _timerService.IdProvider.GetCreatureAbilityCooldownTimerId(creatureControllerInstanceId, abilityId);
            _timerService.StartTimer(timerId, basicAttackAbility.CooldownSeconds);
        }
    }
}