using _Project.Scripts.GameScene.Creatures.Basis.Components;
using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.Project.Animations;
using _Project.Scripts.Project.Enums;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Enemy.States
{
    public interface IEnemyStateDeath : IEnemyStateControllerBase
    { 
    }

    public class EnemyStateDeath : EnemyStateControllerBase<EnemyStateDeath>, IEnemyStateDeath
    {
        public override StateMachineStateType StateType => StateMachineStateType.CreatureState_Death;

        private ICreatureComponentAnimator _componentAnimator;

        public EnemyStateDeath(IEnemyCreatureController creatureController) : base(creatureController)
        {
        }

        protected override void OnInit()
        {
            _componentAnimator = _creatureComponentContainer.GetComponent<CreatureComponentAnimator>(CreatureComponentType.AnimatorController);
        }

        protected override void OnFlush()
        {
            _componentAnimator = null;
        }

        protected override void OnEnter()
        {
            _componentAnimator.AnimatorController.Play(AnimatorStateHash.Death);
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
                case CreatureAnimationEvent.Animation_Finished:
                    _creatureController.CreaturePrefab.SetActive(false);
                    _creatureStateMachine.EnterState(StateMachineStateType.CreatureState_Despawn);
                    break;
            }
        }
    }
}