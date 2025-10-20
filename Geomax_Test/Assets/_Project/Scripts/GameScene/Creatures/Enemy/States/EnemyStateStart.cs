using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.Project.Enums;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Enemy.States
{
    public interface IEnemyStateStart : IEnemyStateControllerBase
    { 
    }

    public class EnemyStateStart : EnemyStateControllerBase<EnemyStateStart>, IEnemyStateStart
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Start;

        public EnemyStateStart(IEnemyCreatureController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
        }

        protected override void OnEnter()
        {
            _creatureStateMachine.EnterState(StateMachineStateType.CreatureState_Idle);
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