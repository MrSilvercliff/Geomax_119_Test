using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Enemy
{
    public interface IEnemyCreatureController : ICreatureController
    { 
    }

    public class EnemyCreatureController : CreatureController<EnemyCreatureController>, IEnemyCreatureController
    {
        public override CreatureType CreatureType => CreatureType.ENEMY;

        protected override void OnCreateProcess()
        {
        }

        protected override void OnSpawnedProcess()
        {
        }

        protected override void OnDespawnedProcess()
        {
        }
    }
}