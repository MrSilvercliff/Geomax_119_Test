using _Project.Scripts.Project.Services.Balance.Models;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.BalanceStorage.Async;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Balance.Storages
{
    public interface ICreatureResourceBalanceStorage : IBalanceStorageDictionaryAsyncBase<ICreatureResourceBalanceModel, CreatureResourceBalanceModel>
    { 
    }

    public class CreatureResourceBalanceStorage : BalanceStorageDictionaryAsyncBase<ICreatureResourceBalanceModel, CreatureResourceBalanceModel>, ICreatureResourceBalanceStorage
    {
        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        protected override void OnBalanceModelAdded(ICreatureResourceBalanceModel balanceModel)
        {
        }
    }
}