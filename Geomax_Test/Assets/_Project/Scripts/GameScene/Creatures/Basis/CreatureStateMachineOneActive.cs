using _Project.Scripts.GameScene.Creatures.Basis.States;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Basis
{
    public abstract class CreatureStateMachineOneActive<TCreatureControllerType, TStateControllerType, TFactoryType> : 
        StateMachineOneActive<TStateControllerType, TFactoryType>, ICreatureStateMachine
        where TCreatureControllerType : ICreatureController
        where TStateControllerType : class, ICreatureStateControllerBase
        where TFactoryType : class, IStateMachineBase
    {
        protected List<StateMachineStateType> _activeStates = new();
        protected TCreatureControllerType _creatureController;

        protected override void OnFlush()
        {
            base.OnFlush();
            _creatureController = default;
        }

        public void Setup(ICreatureController creatureController)
        {
            _creatureController = (TCreatureControllerType)creatureController;
        }

        public override void EnterState(StateMachineStateType state)
        {
            base.EnterState(state);

            _activeStates.Clear();

            if (_activeStateController != null)
                _activeStates.Add(_activeStateController.StateType);
        }

        public List<StateMachineStateType> GetActiveStates()
        {
            return _activeStates;
        }

        public bool TryGetState(StateMachineStateType state, out ICreatureStateControllerBase stateController)
        {
            var result = _allStates.TryGetValue(state, out var stateControllerBase);
            stateController = stateControllerBase;
            return result;
        }

        public bool TryGetActiveState(StateMachineStateType state, out ICreatureStateControllerBase stateController)
        {
            if (_activeStateController == null)
            {
                stateController = null;
                return false;
            }

            if (_activeStateController.StateType != state) 
            {
                stateController = null;
                return false;
            }

            stateController = _activeStateController;
            return true;
        }

        public void OnAnimationEvent(CreatureAnimationEvent creatureAnimationEvent)
        {
            _activeStateController?.OnAnimationEvent(creatureAnimationEvent);
        }
    }
}