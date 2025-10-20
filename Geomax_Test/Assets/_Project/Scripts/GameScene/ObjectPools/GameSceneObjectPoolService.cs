using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Basis.Prefab;
using _Project.Scripts.GameScene.Creatures.Enemy;
using _Project.Scripts.GameScene.Creatures.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.ObjectPools
{
    public interface IGameSceneObjectPoolService
    {
        ICreaturePrefabPool CreaturePrefabPool { get; }
        PlayerController.Pool PlayerControllerPool { get; }
        EnemyCreatureController.Pool EnemyControllerPool { get; }
    }

    public class GameSceneObjectPoolService : IGameSceneObjectPoolService
    {
        public ICreaturePrefabPool CreaturePrefabPool => _creaturePrefabPool;
        public PlayerController.Pool PlayerControllerPool => _playerControllerPool;
        public CreatureController<EnemyCreatureController>.Pool EnemyControllerPool => _enemyControllerPool;

        [Inject] private ICreaturePrefabPool _creaturePrefabPool;
        [Inject] private PlayerController.Pool _playerControllerPool;
        [Inject] private EnemyCreatureController.Pool _enemyControllerPool; 
    }
}