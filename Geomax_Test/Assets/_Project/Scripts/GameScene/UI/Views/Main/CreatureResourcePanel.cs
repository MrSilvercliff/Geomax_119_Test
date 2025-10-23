using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.ObjectPools;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;
using IInitializable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync.IInitializable;

namespace _Project.Scripts.GameScene.UI.Views.Main
{
    public class CreatureResourcePanel : MonoBehaviour, IInitializable, IFlushable, IMonoUpdatable
    {
        [SerializeField] private RectTransform _container;

        [Inject] private ICameraController _cameraController;
        [Inject] private IGameSceneObjectPoolService _gameSceneObjectPoolService;
        
        private Vector2 _offsetMultiplier;
        private Dictionary<int, CreatureResourceContainerWidget> _creatureResourceContainerByInstanceId;
        private List<CreatureResourceContainerWidget> _creatureResourceContainerList;

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        public bool Init()
        {
            var screen = new Vector2(Screen.width, Screen.height);
            _offsetMultiplier = new Vector2(_container.rect.width / screen.x, _container.rect.height / screen.y);

            _creatureResourceContainerByInstanceId = new();
            _creatureResourceContainerList = new();
            return true;
        }

        public bool Flush()
        {
            return true;
        }

        public void Setup(IReadOnlyCollection<ICreatureController> creatureControllers)
        {
            foreach (var creatureController in creatureControllers)
            {
                if (creatureController.CreatureType != CreatureType.PLAYER)
                    continue;

                SpawnCreatureResourceContainer(creatureController);
            }
        }

        private void SpawnCreatureResourceContainer(ICreatureController creatureController)
        {
            var creatureControllerInstanceId = creatureController.InstanceID;
            var anchor = creatureController.ResourceContainerAnchor;

            var anchorScreenPosition = _container.InverseTransformPoint(anchor.position);
            var anchoredX = anchorScreenPosition.x * _offsetMultiplier.x;
            var anchoredY = anchorScreenPosition.y * _offsetMultiplier.y;
            var anchoredPosition = new Vector2(anchoredX, anchoredY);

            var pool = _gameSceneObjectPoolService.CreatureResourceContainerPool;

            var newContainer = pool.Spawn();
            newContainer.Setup(creatureController);
            newContainer.Transform.SetParent(_container);
            newContainer.Transform.ResetLocalPosition();
            newContainer.Transform.ResetLocalRotation();
            newContainer.Transform.ResetLocalScale();
            newContainer.RectTransform.anchoredPosition = anchorScreenPosition;
            newContainer.SetActive(true);

            _creatureResourceContainerByInstanceId[creatureControllerInstanceId] = newContainer;
            _creatureResourceContainerList.Add(newContainer);
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var creatureResourceContainer in _creatureResourceContainerList)
                creatureResourceContainer.OnUpdate(deltaTime);
        }
    }
}