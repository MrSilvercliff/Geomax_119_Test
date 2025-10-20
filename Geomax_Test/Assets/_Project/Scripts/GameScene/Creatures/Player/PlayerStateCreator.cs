using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Player.States;
using _Project.Scripts.Project.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Creatures.Player
{
    public interface IPlayerStateCreator : ICreatureStateCreator<IPlayerController, IPlayerStateControllerBase>
    {

    }

    public class PlayerStateCreator : IPlayerStateCreator
    {
        [Inject] private PlayerStateStart.Factory _startFactory;
        [Inject] private PlayerStateIdle.Factory _idleFactory;
        [Inject] private PlayerStateAttack.Factory _attackFactory;
        [Inject] private PlayerStateDeath.Factory _deathFactory;

        public IPlayerStateControllerBase Create(StateMachineStateType state, IPlayerController creatureController)
        {
            IPlayerStateControllerBase result = null;

            switch (state)
            {
                case StateMachineStateType.CreatureState_Start:
                    result = _startFactory.Create(creatureController);
                    break;

                case StateMachineStateType.CreatureState_Idle:
                    result = _idleFactory.Create(creatureController);
                    break;

                case StateMachineStateType.CreatureState_Attack:
                    result = _attackFactory.Create(creatureController);
                    break;

                case StateMachineStateType.CreatureState_Death:
                    result = _deathFactory.Create(creatureController);
                    break;

                default:
                    LogUtils.Error(this, $"Create not implemented for state [{state}]");
                    break;
            }

            return result;
        }
    }
}