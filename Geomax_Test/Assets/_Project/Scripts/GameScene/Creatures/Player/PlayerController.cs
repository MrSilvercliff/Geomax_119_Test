using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Player
{
    public interface IPlayerController : ICreatureController
    { 
    }

    public class PlayerController : CreatureController<PlayerController>, IPlayerController
    {
        public override CreatureType CreatureType => CreatureType.PLAYER;

        #region BASIS

        protected override void OnCreateProcess()
        {
        }

        protected override void OnSpawnedProcess()
        {
        }

        protected override void OnDespawnedProcess()
        {
        }

        #endregion BASIS

        #region GAMEPLAY
        #endregion GAMEPLAY

        #region INPUT
        #endregion INPUT
    }
}