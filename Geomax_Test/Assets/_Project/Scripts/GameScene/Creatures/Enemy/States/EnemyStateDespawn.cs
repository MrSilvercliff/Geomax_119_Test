using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.GameScene.Services.Creatures;
using _Project.Scripts.Project.Enums;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Enemy.States
{
    public interface IEnemyStateDespawn : IEnemyStateControllerBase
    { 
    }

    public class EnemyStateDespawn : EnemyStateControllerBase<EnemyStateDespawn>, IEnemyStateDespawn
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Despawn;

        [Inject] private ICreatureService _creatureService;

        public EnemyStateDespawn(IEnemyCreatureController creatureController) : base(creatureController)
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
            _creatureService.DespawnCreature(_creatureController);
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
    }
}