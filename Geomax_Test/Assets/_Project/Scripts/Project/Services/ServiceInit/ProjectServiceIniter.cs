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
        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        public override async Task<bool> InitServices(int stage)
        {
            var result = await InitServices();
            return result;
        }
    }
}