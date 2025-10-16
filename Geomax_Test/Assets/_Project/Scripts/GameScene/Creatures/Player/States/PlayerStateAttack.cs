using _Project.Scripts.Project.Enums;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Player.States
{
    public interface IPlayerStateAttack : IPlayerStateControllerBase
    { 
    }

    public class PlayerStateAttack : PlayerStateControllerBase<PlayerStateAttack>, IPlayerStateAttack
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Attack;

        public PlayerStateAttack(IPlayerController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
        }

        protected override void OnEnter()
        {
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