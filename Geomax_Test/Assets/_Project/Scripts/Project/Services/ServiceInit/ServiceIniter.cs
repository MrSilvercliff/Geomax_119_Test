using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.Project.Services.ServiceInit
{
    public interface IServiceIniter : IProjectService
    {
        Task<bool> InitServices(int stage);
    }

    public abstract class ServiceIniter : IServiceIniter
    {
        private HashSet<IProjectService> _allServices;
        private HashSet<IProjectService> _initedServices;

        public async Task<bool> Init()
        {
            _allServices = new HashSet<IProjectService>();
            _initedServices = new HashSet<IProjectService>();
            var result = await OnInit();
            return result;
        }

        public bool Flush()
        {
            foreach (var service in _initedServices)
                FlushService(service);

            _initedServices.Clear();
            return true;
        }

        protected void AddService(IProjectService service)
        {
            _allServices.Add(service);
        }

        protected async Task<bool> InitServices()
        {
            foreach (var service in _allServices)
            {
                if (_initedServices.Contains(service))
                    continue;

                var initResult = await InitService(service);

                if (!initResult)
                    return initResult;

                _initedServices.Add(service);
            }

            return true;
        }

        private async Task<bool> InitService(IProjectService service)
        {
            var serviceName = service.GetType().Name;

            LogUtils.Info(this, $"[{serviceName}] init START");

            try
            {
                var initResult = await service.Init();

                if (!initResult)
                    return initResult;
            }
            catch (Exception e)
            {
                LogUtils.Error(this, $"[{serviceName}] init ERROR");
                LogUtils.Error(this, e.Message);
                LogUtils.Error(this, e.StackTrace);
            }

            LogUtils.Info(this, $"[{serviceName}] init SUCCESS");
            return true;
        }

        private bool FlushService(IProjectService service)
        {
            var serviceName = service.GetType().Name;

            LogUtils.Info(this, $"[{serviceName}] flush START");

            try
            {
                var flushResult = service.Flush();

                if (!flushResult)
                    return flushResult;
            }
            catch (Exception e)
            {
                LogUtils.Error(this, $"[{serviceName}] flush ERROR");
                LogUtils.Error(this, e.Message);
                LogUtils.Error(this, e.StackTrace);
            }

            LogUtils.Info(this, $"[{serviceName}] flush SUCCESS");
            return true;
        }

        public abstract Task<bool> InitServices(int stage);

        protected abstract Task<bool> OnInit();
    }
}