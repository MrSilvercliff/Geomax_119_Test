using _Project.Scripts.Application.Project.Services.Balance;
using _Project.Scripts.Project.Configs;
using _Project.Scripts.Project.Configs.Windows;
using _Project.Scripts.Project.Handlers.SceneLoading;
using _Project.Scripts.Project.Handlers.UI.Panels;
using _Project.Scripts.Project.Handlers.UI.Popups;
using _Project.Scripts.Project.Handlers.UI.Views;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.SceneLoading;
using _Project.Scripts.Project.Services.ServiceInit;
using _Project.Scripts.Project.Services.UI.Panels;
using _Project.Scripts.Project.Services.UI.Popups;
using _Project.Scripts.Project.Services.UI.Views;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.Configs;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.JSONParse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Configs;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Popups;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Panels;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Popups;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Views;
using ZerglingUnityPlugins.ZenjectExtentions.ContextProvider;

namespace _Project.Scripts.Project.Zenject
{
    /// <summary>
    /// PROJECT ENTRY POINT
    /// There are basic binding for services and config that can be used from ANY scene
    /// </summary>
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private MonoUpdater _monoUpdater;
        [SerializeField] private SceneLoadController _sceneLoadController;
        [SerializeField] private ProjectBalanceConfig _projectBalanceConfig;

        [Header("VIEWS")]
        [SerializeField] private ViewConfig _viewConfig;
        [SerializeField] private ViewController _viewController;

        [Header("POPUPS")]
        [SerializeField] private PopupConfig _popupConfig;
        [SerializeField] private PopupController _popupController;

        [Header("PANELS")]
        [SerializeField] private PanelConfig _panelConfig;
        [SerializeField] private PanelController _panelController;

        public override void InstallBindings()
        {
            BindBasicServices();
            BindProjectServices();
        }

        private void BindBasicServices()
        {
            BindZenjectExtensions();

            BindMonoUpdater();

            BindSceneLoadingServices();

            BindViewServices();

            BindPopupServices();

            BindPanelServices();

            BindBalanceServices();

            BindProjectServiceIniter();
        }

        #region BasicServices

        private void BindZenjectExtensions()
        {
            Container.Bind<IZenjectContextProvider>().To<ZenjectContextProvider>().AsSingle();
        }

        private void BindMonoUpdater()
        {
            Container.Bind<IMonoUpdater>().FromInstance(_monoUpdater).AsSingle();
        }

        private void BindSceneLoadingServices()
        {
            Container.Bind<ISceneControllerProvider>().To<SceneControllerProvider>().AsSingle();
            Container.Bind<ISceneLoadHandler>().To<SceneLoadHandler>().AsSingle();
            Container.Bind<ISceneLoadController>().FromInstance(_sceneLoadController).AsSingle();
        }

        private void BindViewServices()
        {
            Container.Bind<IViewsConfig>().FromInstance(_viewConfig).AsSingle();
            Container.Bind<IViewHandler>().To<ViewHandler>().AsSingle();
            Container.Bind<IViewController>().FromInstance(_viewController).AsSingle();
        }

        private void BindPopupServices()
        {
            Container.BindFactory<PopupWindow, PopupWindow, PopupWindow.Factory>().FromFactory<PopupFactory>();
            Container.Bind<IPopupsConfig>().FromInstance(_popupConfig).AsSingle();
            Container.Bind<IPopupHandler>().To<PopupHandler>().AsSingle();
            Container.Bind<IPopupController>().FromInstance(_popupController).AsSingle();
        }

        private void BindPanelServices()
        {
            Container.Bind<IPanelsConfig>().FromInstance(_panelConfig).AsSingle();
            Container.Bind<IPanelSettingsRepository>().To<PanelSettingsRepository>().AsSingle();
            Container.Bind<IPanelHandler>().To<PanelHandler>().AsSingle();
            Container.Bind<IPanelController>().FromInstance(_panelController).AsSingle();
        }

        private void BindBalanceServices()
        { 
            Container.Bind<IBalanceConfig>().FromInstance(_projectBalanceConfig).AsSingle();
            Container.Bind<IJSONParseHelper>().To<JsonParseHelper>().AsSingle();
            Container.Bind<IBalanceJSONParser>().To<BalanceJSONParser>().AsSingle();
            Container.Bind<IProjectBalanceService>().To<ProjectBalanceService>().AsSingle();
        }

        private void BindProjectServiceIniter()
        { 
            Container.Bind<IProjectServiceIniter>().To<ProjectServiceIniter>().AsSingle();
        }

        #endregion BasicServices

        private void BindProjectServices()
        {
        }
    }
}