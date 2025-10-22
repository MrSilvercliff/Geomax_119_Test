using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Combat
{
    public interface ICombatCreatureSelectService : IProjectService
    {
        ICreatureController GetSelectedCreatureController(CreatureType creatureType);
        void SelectCreatureController(ICreatureController creatureController);
    }

    public class CombatCreatureSelectService : ICombatCreatureSelectService
    {
        private ICreatureController _selectedPlayerCreatureController;
        private ICreatureController _selectedEnemyCreatureController;

        public CombatCreatureSelectService()
        { 
            _selectedPlayerCreatureController = null;
            _selectedEnemyCreatureController = null;
        }

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public ICreatureController GetSelectedCreatureController(CreatureType creatureType)
        {
            ICreatureController result = null;

            switch (creatureType)
            {
                case CreatureType.PLAYER:
                    result = _selectedPlayerCreatureController;
                    break;

                case CreatureType.ENEMY:
                    result = _selectedEnemyCreatureController;
                    break;
            }

            return result;
        }

        public void SelectCreatureController(ICreatureController creatureController)
        {
            var creatureType = creatureController.CreatureType;

            switch (creatureType)
            {
                case CreatureType.PLAYER:
                    SelectPlayerCreature(creatureController);
                    break;

                case CreatureType.ENEMY:
                    SelectEnemyCreature(creatureController);
                    break;
            }
        }

        private void SelectPlayerCreature(ICreatureController creatureController)
        {
            _selectedPlayerCreatureController = creatureController;

            if (_selectedPlayerCreatureController.AttackTargetCreatureController != null) 
                _selectedEnemyCreatureController = _selectedPlayerCreatureController.AttackTargetCreatureController;
        }

        private void SelectEnemyCreature(ICreatureController creatureController)
        {
            _selectedEnemyCreatureController = creatureController;

            if (_selectedPlayerCreatureController != null)
                _selectedPlayerCreatureController.SetAttackTarget(creatureController);
        }
    }
}