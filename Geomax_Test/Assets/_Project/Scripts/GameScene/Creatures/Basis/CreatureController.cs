using _Project.Scripts.GameScene.Creatures.Basis.Components;
using _Project.Scripts.GameScene.Services.Creatures;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Extensions;
using _Project.Scripts.Project.Monobeh;
using _Project.Scripts.Project.ObjectPools;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Log;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.Creatures.Basis
{
    public interface ICreatureController : IProjectMonoBehaviour, IProjectPoolable,  IMonoUpdatable, IMonoFixedUpdatable, IMonoLateUpdatable
    {
        CreatureType CreatureType { get; }

        StateMachineStateType OnSpawnState { get; }

        ICreatureModel CreatureModel { get; }
        ICreatureStateMachine CreatureStateMachine { get; }
        ICreatureComponentContainer CreatureComponentContainer { get; }

        void InitComponents();
        void SetupModel(ICreatureModel model);
        void SetupStateMachine(ICreatureStateMachine stateMachine);
        void SetupPrefab(ICreaturePrefab view);

        void OnAnimationEvent(CreatureAnimationEvent creatureAnimationEvent);
    }

    public abstract class CreatureController : ProjectMonoBehaviour, ICreatureController
    {
        public abstract CreatureType CreatureType { get; }

        public StateMachineStateType OnSpawnState => _onSpawnState;

        public ICreatureModel CreatureModel => _creatureModel;
        public ICreatureStateMachine CreatureStateMachine => _creatureStateMachine;
        public ICreatureComponentContainer CreatureComponentContainer => _componentContainer;

        [Header("CREATURE CONTROLLER")]
        [SerializeField] private Transform _prefabContainer;
        [SerializeField] private StateMachineStateType _onSpawnState;
        [SerializeField] private CreatureComponentBase[] _componentsList;

        [Inject] private ICreatureControllerRepository _creatureControllerRepository;

        protected ICreatureModel _creatureModel;
        protected ICreaturePrefab _creaturePrefab;
        protected ICreatureStateMachine _creatureStateMachine;
        protected ICreatureComponentContainer _componentContainer;

        #region BASIS

        protected override void OnAwake()
        {
        }

        public void InitComponents()
        {
            _componentContainer.InitComponents(this, _componentsList);
        }

        public void SetupModel(ICreatureModel model)
        {
            _creatureModel = model;
        }

        public void SetupStateMachine(ICreatureStateMachine stateMachine) 
        {
            _creatureStateMachine = stateMachine;
            _creatureStateMachine.Setup(this);
            _creatureStateMachine.Init();
        }

        public void SetupPrefab(ICreaturePrefab prefab)
        {
            _creaturePrefab = prefab;
            _creaturePrefab.Transform.SetParent(_prefabContainer);
            _creaturePrefab.Transform.ResetLocalPosition();
            _creaturePrefab.Transform.ResetLocalRotation();
            _creaturePrefab.Transform.ResetLocalScale();
            _componentContainer.InitComponents(this, _creaturePrefab.Components);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            _creatureStateMachine.OnFixedUpdate(deltaTime);
        }

        public void OnUpdate(float deltaTime)
        {
            _creatureStateMachine.OnUpdate(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            _creatureStateMachine.OnLateUpdate(deltaTime);
        }

        public void OnCreated()
        {
            LogUtils.Info(gameObject.name, $"OnCreated");
            
            _componentContainer = new CreatureComponentContainer();

            OnCreateProcess();
        }

        protected abstract void OnCreateProcess();

        public void OnSpawned()
        {
            LogUtils.Info(gameObject.name, $"OnSpawned");

            OnSpawnedProcess();
            _creatureControllerRepository.Add(this);
        }

        protected abstract void OnSpawnedProcess();

        public void OnDespawned()
        {
            LogUtils.Info(gameObject.name, $"OnDespawned");

            _componentContainer.Flush();
            OnDespawnedProcess();
            _creatureControllerRepository.Remove(this);
        }

        protected abstract void OnDespawnedProcess();

        public void OnAnimationEvent(CreatureAnimationEvent creatureAnimationEvent)
        {
            _creatureStateMachine.OnAnimationEvent(creatureAnimationEvent);
        }

        #endregion BASIS

        #region GAMEPLAY
        #endregion GAMEPLAY
    }

    public abstract class CreatureController<TObjectPoolType> : CreatureController
        where TObjectPoolType : CreatureController
    { 
        public class Pool : ProjectMonoMemoryPool<TObjectPoolType> { }
    }
}