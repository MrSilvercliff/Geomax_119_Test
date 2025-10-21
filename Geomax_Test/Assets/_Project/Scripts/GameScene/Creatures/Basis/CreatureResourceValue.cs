using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance.Models;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Basis
{
    public interface ICreatureResourceValue
    { 
        CreatureResourceType ResourceType { get; }
        int MinValue { get; }
        int MaxValue { get; }
        int CurrentValue { get; }

        void SetCurrentValue(int newValue);
    }

    public class CreatureResourceValue : ICreatureResourceValue
    {
        public CreatureResourceType ResourceType => _balanceModel.ResourceType;
        public int MinValue => _balanceModel.MinValue;
        public int MaxValue => _balanceModel.MaxValue;
        public int CurrentValue => _currentValue;

        private ICreatureResourceBalanceModel _balanceModel;
        private int _currentValue;

        public CreatureResourceValue(ICreatureResourceBalanceModel balanceModel)
        { 
            _balanceModel = balanceModel;
            _currentValue = _balanceModel.StartValue;
        }

        public void SetCurrentValue(int newValue)
        {
            _currentValue = newValue;

            if (_currentValue < MinValue)
                _currentValue = MinValue;
            else if (_currentValue > MaxValue)
                _currentValue = MaxValue;
        }

        public class Factory : PlaceholderFactory<ICreatureResourceBalanceModel, CreatureResourceValue> { }
    }
}