using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.Project.Enums;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Enemy.States
{
    public interface IEnemyStateDespawn : IEnemyStateControllerBase
    { 
    }

    public class EnemyStateDespawn : EnemyStateControllerBase<EnemyStateDespawn>, IEnemyStateDespawn
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Despawn;

        public EnemyStateDespawn(IEnemyCreatureController creatureController) : base(creatureController)
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