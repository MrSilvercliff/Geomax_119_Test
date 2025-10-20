using _Project.Scripts.GameScene.Creatures.Basis.States;
using _Project.Scripts.GameScene.Creatures.Player.States;
using _Project.Scripts.GameScene.Creatures.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Enemy.States
{
    public interface IEnemyStateControllerBase : ICreatureStateControllerBase
    { 
    }

    public abstract class EnemyStateControllerBase<TFactoryType> : CreatureStateControllerBase<IPlayerController>, IEnemyStateControllerBase
        where TFactoryType : IEnemyStateControllerBase
    {
        protected EnemyStateControllerBase(IPlayerController creatureController) : base(creatureController)
        {
        }

        public class Factory : PlaceholderFactory<IEnemyCreatureController, TFactoryType> { }
    }
}