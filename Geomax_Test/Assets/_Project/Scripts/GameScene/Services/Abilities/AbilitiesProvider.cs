using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.Balance.Models;
using _Project.Scripts.Project.Services.Balance.Storages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.Abilities
{
    public interface IAbilitiesProvider : IProjectService
    {
        IAbility GetAbility(string id);
    }

    public class AbilitiesProvider : IAbilitiesProvider
    {
        [Inject] private IAbilitiesCreator _abilitiesCreator;

        private Dictionary<string, IAbility> _abilitiesById;

        public AbilitiesProvider()
        {
            _abilitiesById = new();
        }

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public IAbility GetAbility(string id)
        {
            var tryGetResult = _abilitiesById.TryGetValue(id, out var existAbility);

            if (tryGetResult)
                return existAbility;

            var newAbility = _abilitiesCreator.CreateAbility(id);
            _abilitiesById[id] = newAbility;
            return newAbility;
        }
    }
}