using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Enemy.States;
using _Project.Scripts.Project.Enums;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Creatures.Enemy
{
    public interface IEnemyCreatureStateCreator : ICreatureStateCreator<IEnemyCreatureController, IEnemyStateControllerBase>
    { 
    }

    public class EnemyCreatureStateCreator : IEnemyCreatureStateCreator
    {
        [Inject] private EnemyStateStart.Factory _startFactory;
        [Inject] private EnemyStateIdle.Factory _idleFactory;
        [Inject] private EnemyStateAttack.Factory _attackFactory;
        [Inject] private EnemyStateHit.Factory _hitFactory;
        [Inject] private EnemyStateDeath.Factory _deathFactory;
        [Inject] private EnemyStateDespawn.Factory _despawnFactory;

        public IEnemyStateControllerBase Create(StateMachineStateType state, IEnemyCreatureController creatureController)
        {
            IEnemyStateControllerBase result = null;

            switch (state)
            {
                case StateMachineStateType.CreatureState_Start:
                    result = _startFactory.Create(creatureController);
                    break;

                case StateMachineStateType.CreatureState_Idle:
                    result = _idleFactory.Create(creatureController);
                    break;

                case StateMachineStateType.CreatureState_Attack:
                    result = _attackFactory.Create(creatureController);
                    break;

                case StateMachineStateType.CreatureState_Hit:
                    result = _hitFactory.Create(creatureController);
                    break;

                case StateMachineStateType.CreatureState_Death:
                    result = _deathFactory.Create(creatureController);
                    break;

                case StateMachineStateType.CreatureState_Despawn:
                    result = _despawnFactory.Create(creatureController);
                    break;

                default:
                    LogUtils.Error(this, $"Create not implemented for state [{state}]");
                    break;
            }

            return result;
        }
    }
}