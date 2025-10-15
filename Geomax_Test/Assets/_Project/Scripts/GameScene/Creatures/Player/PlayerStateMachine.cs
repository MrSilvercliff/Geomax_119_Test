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
        }
    }
}