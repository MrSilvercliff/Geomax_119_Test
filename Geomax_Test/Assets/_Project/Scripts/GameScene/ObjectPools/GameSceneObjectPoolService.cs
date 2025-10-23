using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Basis.Prefab;
using _Project.Scripts.GameScene.Creatures.Enemy;
using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.GameScene.Effects.CreatureEffects;
using _Project.Scripts.GameScene.UI.Views.Main;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.ObjectPools
{
    public interface IGameSceneObjectPoolService
    {
        ICreaturePrefabPool CreaturePrefabPool { get; }
        PlayerController.Pool PlayerControllerPool { get; }
        EnemyCreatureController.Pool EnemyControllerPool { get; }
        CreatureEffectController.Pool CreatureEffectControllerPool { get; }
        CreatureResourceContainerWidget.Pool CreatureResourceContainerPool { get; }
        CreatureResourceWidget.Pool CreatureResourceWidgetPool { get; }
    }

    public class GameSceneObjectPoolService : IGameSceneObjectPoolService
    {
        public ICreaturePrefabPool CreaturePrefabPool => _creaturePrefabPool;
        public PlayerController.Pool PlayerControllerPool => _playerControllerPool;
        public EnemyCreatureController.Pool EnemyControllerPool => _enemyControllerPool;
        public CreatureEffectController.Pool CreatureEffectControllerPool => _creatureEffectControllerPool;
        public CreatureResourceContainerWidget.Pool CreatureResourceContainerPool => _creatureResourceContainerPool;
        public CreatureResourceWidget.Pool CreatureResourceWidgetPool => _creatureResourceWidgetPool;

        [Inject] private ICreaturePrefabPool _creaturePrefabPool;
        [Inject] private PlayerController.Pool _playerControllerPool;
        [Inject] private EnemyCreatureController.Pool _enemyControllerPool;
        [Inject] private CreatureEffectController.Pool _creatureEffectControllerPool;
        [Inject] private CreatureResourceContainerWidget.Pool _creatureResourceContainerPool;
        [Inject] private CreatureResourceWidget.Pool _creatureResourceWidgetPool;
    }
}