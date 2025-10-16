using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Basis.Prefab;
using _Project.Scripts.GameScene.Creatures.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.ObjectPools
{
    public interface IGameSceneObjectPoolService
    {
        ICreaturePrefabPool CreaturePrefabPool { get; }
        PlayerController.Pool PlayerControllerPool { get; }
    }

    public class GameSceneObjectPoolService : IGameSceneObjectPoolService
    {
        public ICreaturePrefabPool CreaturePrefabPool => _creaturePrefabPool;
        public PlayerController.Pool PlayerControllerPool => _playerControllerPool;

        [Inject] private ICreaturePrefabPool _creaturePrefabPool;
        [Inject] private PlayerController.Pool _playerControllerPool;
    }
}