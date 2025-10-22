using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.GameScene.Creatures.Basis.Components;
using _Project.Scripts.GameScene.Services.Combat;
using _Project.Scripts.GameScene.Services.Creatures;
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
        [Inject] private ICreatureService _creatureService;
        [Inject] private ICombatService _combatService;

        private ICreatureComponentAnimator _componentAnimator;
        private IAbilityAttack _abilityAttack;
        private IAbilityResult _abilityResult;

        public PlayerStateAttack(IPlayerController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
            _componentAnimator = _creatureComponentContainer.GetComponent<ICreatureComponentAnimator>(CreatureComponentType.AnimatorController);
            _abilityAttack = _creatureModel.GetBasicAttackAbility();
        }

        protected override void OnFlush()
        {
            _componentAnimator = null;
            _abilityAttack = null;

            _abilityResult.Flush();
            _abilityResult = null;
        }

        protected override void OnEnter()
        {
            _creatureController.CheckAttackTarget();

            if (_creatureController.AttackTargetCreatureController == null)
            {
                _creatureStateMachine.EnterState(StateMachineStateType.CreatureState_Idle);
                return;
            }

            _componentAnimator.AnimatorController.Play(AnimatorStateHash.Attack);
            StartBasicAttackCooldownTimer();
            _abilityResult = _combatService.UseAbility(_creatureController, _abilityAttack);
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
            _abilityResult.Flush();
            _abilityResult = null;
        }

        public override void OnAnimationEvent(CreatureAnimationEvent creatureAnimationEvent)
        {
            switch (creatureAnimationEvent) 
            {
                case CreatureAnimationEvent.Attack_Deal_Damage:
                    _combatService.ApplyAbilityResult(_abilityResult);
                    _creatureController.CheckAttackTarget();
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