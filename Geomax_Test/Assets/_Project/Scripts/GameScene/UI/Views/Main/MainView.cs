using _Project.Scripts.GameScene.Services.Creatures;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Basics;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Views;

namespace _Project.Scripts.GameScene.UI.Views.Main
{
    public class MainView : ViewWindowWithSafeArea, IMonoUpdatable
    {
        [SerializeField] private CreatureResourcePanel _creatureResourcePanel;

        [Inject] private IMonoUpdater _updater;
        [Inject] private ICreatureService _creatureService;

        protected override async Task<bool> OnInit()
        {
            await base.OnInit();
            _creatureResourcePanel.Init();
            return true;
        }

        protected override bool OnFlush()
        {
            _creatureResourcePanel.Flush();
            return true;
        }

        protected override Task OnPreOpen()
        {
            _updater.Subscribe(this);
            return base.OnPreOpen();
        }

        protected override Task OnPreClose()
        {
            _updater.UnSubscribe(this);
            return base.OnPreClose();
        }

        protected override Task<bool> OnSetup(IWindowSetup setup)
        {
            SetupCreatureResourcePanel();
            return Task.FromResult(true);
        }

        private void SetupCreatureResourcePanel()
        {
            var creatureControllers = _creatureService.GetAllCreatureControllers();
            _creatureResourcePanel.Setup(creatureControllers);
        }

        public void OnUpdate(float deltaTime)
        {
            _creatureResourcePanel.OnUpdate(deltaTime);
        }
    }
}