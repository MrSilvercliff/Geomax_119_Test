using _Project.Scripts.GameScene.Creatures.Basis.Components;
using _Project.Scripts.Project.Animations;
using _Project.Scripts.Project.Enums;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Player.States
{
    public interface IPlayerStateIdle : IPlayerStateControllerBase
    { 
    }

    public class PlayerStateIdle : PlayerStateControllerBase<PlayerStateIdle>, IPlayerStateIdle
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Idle;

        private IMonoBehaviourAnimatorController _animatorController;

        public PlayerStateIdle(IPlayerController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
            var animatorComponent = _creatureComponentContainer.GetComponent<CreatureComponentAnimator>(CreatureComponentType.AnimatorController);
            _animatorController = animatorComponent.AnimatorController;
        }

        protected override void OnEnter()
        {
            _animatorController.Play(AnimatorStateHash.Idle);
        }

        public override void OnFixedUpdate()
        {
        }

        public override void OnUpdate()
        {
        }

        public override void OnLateUpdate()
        {
        }

        protected override void OnExit()
        {
        }

        public override void OnAnimationFinished(int finishedState)
        {
        }
    }
}