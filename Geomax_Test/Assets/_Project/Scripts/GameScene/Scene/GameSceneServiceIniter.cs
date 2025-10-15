using _Project.Scripts.GameScene.Services.Creatures;
using System.Threading.Tasks;
using Zenject;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Panels;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Popups;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Views;
using _Project.Scripts.Project.Services.ServiceInit;

namespace _Project.Scripts.GameScene.Scene
{
    public interface IGameSceneServiceIniter : IServiceIniter
    { 
    }

    public class GameSceneServiceIniter : ServiceIniter, IGameSceneServiceIniter
    {
        #region First

        // windows
        [Inject] private IViewController _viewController;
        [Inject] private IPopupController _popupController;
        [Inject] private IPanelSettingsRepository _panelSettingsRepository;
        [Inject] private IPanelController _panelController;

        #endregion First

        #region Second

        // creatures
        [Inject] private ICreatureControllerRepository _creatureControllerRepository;

        #endregion Second

        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        public override async Task<bool> InitServices(int stage)
        {
            var result = true;

            switch (stage)
            {
                case 1:
                    result = await InitFirst();
                    break;

                case 2:
                    result = await InitSecond();
                    break;
            }

            return result;
        }

        private async Task<bool> InitFirst()
        {
            AddService(_viewController);
            AddService(_popupController);
            AddService(_panelSettingsRepository);
            AddService(_panelController);

            var result = await InitServices();
            return result;
        }

        private async Task<bool> InitSecond()
        {
            AddService(_creatureControllerRepository);

            var result = await InitServices();
            return result;
        }
    }
}