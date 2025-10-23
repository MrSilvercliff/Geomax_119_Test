using _Project.Scripts.GameScene.Creatures.Basis.Components;
using _Project.Scripts.GameScene.Services.Combat;
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
        ICreaturePrefab CreaturePrefab { get; }
        ICreatureStateMachine CreatureStateMachine { get; }
        ICreatureComponentContainer CreatureComponentContainer { get; }
        ICreatureController AttackTargetCreatureController { get; }

        Transform ResourceContainerAnchor { get; }

        #region BASIS

        void InitComponents();
        void SetupModel(ICreatureModel model);
        void SetupStateMachine(ICreatureStateMachine stateMachine);
        void SetupPrefab(ICreaturePrefab view);

        void OnAnimationEvent(CreatureAnimationEvent creatureAnimationEvent);

        #endregion BASIS


        #region GAMEPLAY

        bool IsAlive();
        void CheckAttackTarget();
        void SetAttackTarget(ICreatureController attackTargetCreatureController);
        void OnHit();
        void OnDeath();

        #endregion GAMEPLAY
    }

    public abstract class CreatureController : ProjectMonoBehaviour, ICreatureController
    {
        public abstract CreatureType CreatureType { get; }

        public StateMachineStateType OnSpawnState => _onSpawnState;

        public ICreatureModel CreatureModel => _creatureModel;
        public ICreaturePrefab CreaturePrefab => _creaturePrefab;
        public ICreatureStateMachine CreatureStateMachine => _creatureStateMachine;
        public ICreatureComponentContainer CreatureComponentContainer => _componentContainer;
        public ICreatureController AttackTargetCreatureController => _attackTargetCreatureController;

        public Transform ResourceContainerAnchor => _resourceContainerAnchor;

        [Header("CREATURE CONTROLLER")]
        [SerializeField] private Transform _prefabContainer;
        [SerializeField] private Transform _resourceContainerAnchor;
        [SerializeField] private StateMachineStateType _onSpawnState;
        [SerializeField] private CreatureComponentBase[] _componentsList;

        [Inject] private ICombatService _combatService;

        protected ICreatureModel _creatureModel;
        protected ICreaturePrefab _creaturePrefab;
        protected ICreatureStateMachine _creatureStateMachine;
        protected ICreatureComponentContainer _componentContainer;

        protected ICreatureController _attackTargetCreatureController;

        #region BASIS

        protected override void OnAwake()
        {
            base.OnAwake();
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
            _creaturePrefab.SetActive(true);
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
            _attackTargetCreatureController = null;

            OnCreateProcess();
        }

        protected abstract void OnCreateProcess();

        public void OnSpawned()
        {
            LogUtils.Info(gameObject.name, $"OnSpawned");

            OnSpawnedProcess();
        }

        protected abstract void OnSpawnedProcess();

        public void OnDespawned()
        {
            LogUtils.Info(gameObject.name, $"OnDespawned");

            _componentContainer.Flush();
            _creatureStateMachine.Flush();
            OnDespawnedProcess();
        }

        protected abstract void OnDespawnedProcess();

        public void OnAnimationEvent(CreatureAnimationEvent creatureAnimationEvent)
        {
            _creatureStateMachine.OnAnimationEvent(creatureAnimationEvent);
        }

        #endregion BASIS


        #region GAMEPLAY

        public bool IsAlive()
        {
            var hitPoints = _creatureModel.GetResourceValue(CreatureResourceType.HIT_POINTS);
            var result = hitPoints.CurrentValue > hitPoints.MinValue;
            return result;
        }

        public void CheckAttackTarget()
        {
            if (_attackTargetCreatureController == null)
            {
                _attackTargetCreatureController = _combatService.GetAttackTargetForCreature(CreatureType);
                return;
            }
            
            var isAlive = _attackTargetCreatureController.IsAlive();

            if (isAlive)
                return;

            _attackTargetCreatureController = _combatService.GetAttackTargetForCreature(CreatureType);
        }

        public void SetAttackTarget(ICreatureController attackTargetCreatureController)
        {
            _attackTargetCreatureController = attackTargetCreatureController;
        }

        public void OnHit()
        {
            _creatureStateMachine.EnterState(StateMachineStateType.CreatureState_Hit);
        }

        public void OnDeath()
        {
            _creatureStateMachine.EnterState(StateMachineStateType.CreatureState_Death);
        }

        #endregion GAMEPLAY
    }

    public abstract class CreatureController<TObjectPoolType> : CreatureController
        where TObjectPoolType : CreatureController
    { 
        public class Pool : ProjectMonoMemoryPool<TObjectPoolType> 
        {
            protected override void OnSpawned(TObjectPoolType item)
            {
                base.OnSpawned(item);
                item.gameObject.name = $"[CreatureController]_{item.CreatureType}_[{item.InstanceID}]";
            }
        }
    }
}