using _Project.Scripts.Project.Enums;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Player.States
{
    public interface IPlayerStateStart : IPlayerStateControllerBase
    { 
    }

    public class PlayerStateStart : PlayerStateControllerBase<PlayerStateStart>, IPlayerStateStart
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Start;

        public PlayerStateStart(IPlayerController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
        }

        protected override void OnEnter()
        {
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

        public override void OnAnimationFinished(int finishedState)
        {
        }
    }
}