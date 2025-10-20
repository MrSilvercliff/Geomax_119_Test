using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Player.States;
using _Project.Scripts.Project.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Player
{
    public class PlayerStateMachine : CreatureStateMachineOneActive<IPlayerController, IPlayerStateControllerBase, PlayerStateMachine>
    {
        [Inject] private IPlayerStateCreator _stateCreator;

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

            var deathState = StateMachineStateType.CreatureState_Death;
            _allStates[deathState] = _stateCreator.Create(deathState, _creatureController);
        }
    }
}