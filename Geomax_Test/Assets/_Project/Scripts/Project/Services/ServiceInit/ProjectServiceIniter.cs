using _Project.Scripts.Project.Services.Balance;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Project.Services.ServiceInit
{
    public interface IProjectServiceIniter : IServiceIniter
    {
    }

    /// <summary>
    /// This service will init Project scope services that can be used from any other scene
    /// </summary>
    public class ProjectServiceIniter : ServiceIniter, IProjectServiceIniter
    {
        [Inject] private IProjectBalanceService _projectBalanceService;

        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        public override async Task<bool> InitServices(int stage)
        {
            AddService(_projectBalanceService);

            var result = await InitServices();
            return result;
        }
    }
}