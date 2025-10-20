using _Project.Scripts.GameScene.GameLoop;
using _Project.Scripts.Project.Scenes;
using _Project.Scripts.Project.Services.ServiceInit;
using System.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneController : SceneController
    {
        [Inject] private IProjectServiceIniter _projectServiceIniter;
        [Inject] private IGameSceneServiceIniter _serviceIniter;
        [Inject] private IGameLoopController _gameLoopController;

        protected override async Task OnAwake()
        {
            await _projectServiceIniter.Init();
            await _serviceIniter.Init();

            await _gameLoopController.OnAwake();
        }

        protected override async Task OnStart()
        {
            await _projectServiceIniter.InitServices(0);

            await _serviceIniter.InitServices(1);

            await _serviceIniter.InitServices(2);

            await _gameLoopController.OnStart();
        }

        protected override async Task OnLateStart()
        {
        }

        protected override void OnFlush()
        {
            _gameLoopController.Flush();
            _serviceIniter.Flush();
        }
    }
}