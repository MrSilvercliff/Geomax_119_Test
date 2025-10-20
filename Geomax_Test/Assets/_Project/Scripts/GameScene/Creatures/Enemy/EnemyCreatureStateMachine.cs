using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Enemy.States;
using _Project.Scripts.Project.Enums;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Enemy
{
    public class EnemyCreatureStateMachine : CreatureStateMachineOneActive<IEnemyCreatureController, IEnemyStateControllerBase, EnemyCreatureStateMachine>
    {
        [Inject] private IEnemyCreatureStateCreator _stateCreator;

        protected override void OnInit()
        {
        }

        protected override void CreateStateControllers()
        {
            var startState = StateMachineStateType.CreatureState_Start;
            _allStates[startState] = _stateCreator.Create(startState, _creatureController);

            var idleState = StateMachineStateType.CreatureState_Idle;
            _allStates[idleState] = _stateCreator.Create(idleState, _creatureController);

            var attackState = StateMachineStateType.CreatureState_Attack;
            _allStates[attackState] = _stateCreator.Create(attackState, _creatureController);

            var hitState = StateMachineStateType.CreatureState_Hit;
            _allStates[hitState] = _stateCreator.Create(hitState, _creatureController);

            var deathState = StateMachineStateType.CreatureState_Death;
            _allStates[deathState] = _stateCreator.Create(deathState, _creatureController);

            var despawnState = StateMachineStateType.CreatureState_Despawn;
            _allStates[despawnState] = _stateCreator.Create(despawnState, _creatureController);
        }
    }
}