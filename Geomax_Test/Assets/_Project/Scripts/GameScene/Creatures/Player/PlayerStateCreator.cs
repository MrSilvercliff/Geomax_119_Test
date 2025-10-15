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
        public IPlayerStateControllerBase Create(StateMachineStateType state, IPlayerController creatureController)
        {
            IPlayerStateControllerBase result = null;

            switch (state)
            {
                case StateMachineStateType.CreatureState_Idle:
                    break;

                default:
                    LogUtils.Error(this, $"Create not implemented for state [{state}]");
                    break;
            }

            return result;
        }
    }
}