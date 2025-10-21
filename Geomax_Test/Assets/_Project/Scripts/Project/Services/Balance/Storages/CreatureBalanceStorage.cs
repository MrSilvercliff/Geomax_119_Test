using _Project.Scripts.Project.Services.Balance.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.BalanceStorage.Async;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Balance.Storages
{
    public interface ICreatureBalanceStorage : IBalanceStorageDictionaryAsyncBase<ICreatureBalanceModel, CreatureBalanceModel>
    { 
        IReadOnlyList<ICreatureBalanceModel> Enemies { get; }
    }

    public class CreatureBalanceStorage : BalanceStorageDictionaryAsyncBase<ICreatureBalanceModel, CreatureBalanceModel>, ICreatureBalanceStorage
    {
        public IReadOnlyList<ICreatureBalanceModel> Enemies => _enemies;

        private List<ICreatureBalanceModel> _enemies;

        public CreatureBalanceStorage() : base()
        { 
            _enemies = new();
        }

        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }
        
        protected override void OnBalanceModelAdded(ICreatureBalanceModel balanceModel)
        {
            if (balanceModel.CreatureType == Enums.CreatureType.ENEMY)
                _enemies.Add(balanceModel);
        }
    }
}