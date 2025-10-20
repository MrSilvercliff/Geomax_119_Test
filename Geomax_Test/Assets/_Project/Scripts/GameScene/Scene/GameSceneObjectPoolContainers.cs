using _Project.Scripts.Project.ObjectPools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameScene.Scene
{
    public interface IGameSceneObjectPoolContainers
    {
        ObjectPoolContainerItem PlayerController { get; }
        ObjectPoolContainerItem CreaturePrefab { get; }
        ObjectPoolContainerItem EnemyCreatureController { get; }
    }

    public class GameSceneObjectPoolContainers : MonoBehaviour, IGameSceneObjectPoolContainers
    {
        public ObjectPoolContainerItem PlayerController => _playerController;
        public ObjectPoolContainerItem CreaturePrefab => _creaturePrefab;
        public ObjectPoolContainerItem EnemyCreatureController => _enemyCreatureController;

        [SerializeField] private ObjectPoolContainerItem _playerController;
        [SerializeField] private ObjectPoolContainerItem _creaturePrefab;
        [SerializeField] private ObjectPoolContainerItem _enemyCreatureController;
    }
}