using _Project.Scripts.GameScene.GameLevel;
using _Project.Scripts.GameScene.UI.Views.Main;
using _Project.Scripts.Project.Scenes;
using _Project.Scripts.Project.Services.ServiceInit;
using System.Threading.Tasks;
using Zenject;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Services.Views;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneController : SceneController
    {
        [Inject] private IProjectServiceIniter _projectServiceIniter;
        [Inject] private IGameSceneServiceIniter _serviceIniter;
        [Inject] private IGameLevelController _gameLevelController;
        [Inject] private IViewController _viewController;

        protected override async Task OnAwake()
        {
            await _projectServiceIniter.Init();
            await _serviceIniter.Init();

            await _gameLevelController.OnAwake();
        }

        protected override async Task OnStart()
        {
            await _projectServiceIniter.InitServices(0);

            await _serviceIniter.InitServices(1);

            await _serviceIniter.InitServices(2);

            await _gameLevelController.OnStart();

            await _viewController.OpenView<MainView>();
        }

        protected override async Task OnLateStart()
        {
        }

        protected override void OnFlush()
        {
            _gameLevelController.Flush();
            _serviceIniter.Flush();
        }
    }
}