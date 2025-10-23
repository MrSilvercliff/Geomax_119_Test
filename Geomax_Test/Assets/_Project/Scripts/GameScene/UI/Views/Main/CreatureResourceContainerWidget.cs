using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.ObjectPools;
using _Project.Scripts.Project.Extensions;
using _Project.Scripts.Project.Monobeh;
using _Project.Scripts.Project.ObjectPools;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.UI.Views.Main
{
    public class CreatureResourceContainerWidget : ProjectMonoBehaviour, IProjectPoolable, IMonoUpdatable
    {
        public RectTransform RectTransform => _rectTransform;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Transform _creatureResourceWidgetContainer;

        [Inject] private IGameSceneObjectPoolService _gameSceneObjectPoolService;

        private List<CreatureResourceWidget> _creatureResourceWidgets;

        public void Setup(ICreatureController creatureController)
        { 
            SpawnCreatureResourceWidgets(creatureController);
        }

        private void SpawnCreatureResourceWidgets(ICreatureController creatureController)
        { 
            var resources = creatureController.CreatureModel.GetAllResources();
            var pool = _gameSceneObjectPoolService.CreatureResourceWidgetPool;

            foreach (var resource in resources)
            {
                var newWidget = pool.Spawn();
                newWidget.Setup(resource);
                newWidget.Transform.SetParent(_creatureResourceWidgetContainer);
                newWidget.Transform.ResetLocalPosition();
                newWidget.Transform.ResetLocalRotation();
                newWidget.Transform.ResetLocalScale();
                newWidget.SetActive(true);
                _creatureResourceWidgets.Add(newWidget);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var widget in _creatureResourceWidgets)
                widget.OnUpdate(deltaTime);
        }

        public void OnCreated()
        {
            _creatureResourceWidgets = new();
        }

        public void OnSpawned()
        {
            SetActive(false);
        }

        public void OnDespawned()
        {
            DespawnWidgets();
        }

        private void DespawnWidgets()
        {
            var pool = _gameSceneObjectPoolService.CreatureResourceWidgetPool;

            foreach (var widget in _creatureResourceWidgets)
                pool.Despawn(widget);

            _creatureResourceWidgets.Clear();
        }

        public class Pool : ProjectMonoMemoryPool<CreatureResourceContainerWidget> { }
    }
}