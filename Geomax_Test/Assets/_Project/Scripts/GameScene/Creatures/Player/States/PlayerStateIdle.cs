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

        private ICreatureComponentAnimator _componentAnimator;

        public PlayerStateIdle(IPlayerController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
            _componentAnimator = _creatureComponentContainer.GetComponent<CreatureComponentAnimator>(CreatureComponentType.AnimatorController);
        }

        protected override void OnEnter()
        {
            _componentAnimator.AnimatorController.Play(AnimatorStateHash.Idle);
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

        public override void OnAnimationFinished(int finishedState)
        {
        }
    }
}