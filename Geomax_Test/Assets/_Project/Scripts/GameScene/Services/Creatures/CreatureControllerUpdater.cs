using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICreatureControllerUpdater : IProjectService, ILateStartable, IMonoUpdatable, IMonoFixedUpdatable, IMonoLateUpdatable
    {
    }

    public class CreatureControllerUpdater : ICreatureControllerUpdater
    {
        [Inject] private ICreatureControllerRepository _repository;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public async Task<bool> OnLateStart()
        {
            var controllers = _repository.GetAll();

            foreach (var controller in controllers)
                controller.CreatureStateMachine.EnterState(controller.OnSpawnState);

            return true;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            var controllers = _repository.GetAll();

            foreach (var controller in controllers)
                controller.OnFixedUpdate(deltaTime);
        }

        public void OnUpdate(float deltaTime)
        {
            var controllers = _repository.GetAll();

            foreach (var controller in controllers)
                controller.OnUpdate(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            var controllers = _repository.GetAll();

            foreach (var controller in controllers)
                controller.OnLateUpdate(deltaTime);
        }
    }
}