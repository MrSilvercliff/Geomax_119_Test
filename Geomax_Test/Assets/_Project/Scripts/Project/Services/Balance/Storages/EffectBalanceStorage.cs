using _Project.Scripts.Project.Services.Balance.Models;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.BalanceStorage.Async;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Balance.Storages
{
    public interface IEffectBalanceStorage : IBalanceStorageDictionaryAsyncBase<IEffectBalanceModel, EffectBalanceModel>
    { 
    }

    public class EffectBalanceStorage : BalanceStorageDictionaryAsyncBase<IEffectBalanceModel, EffectBalanceModel>, IEffectBalanceStorage
    {
        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        protected override void OnBalanceModelAdded(IEffectBalanceModel balanceModel)
        {
        }
    }
}