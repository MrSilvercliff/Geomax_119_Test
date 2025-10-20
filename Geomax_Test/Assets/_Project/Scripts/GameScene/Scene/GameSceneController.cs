using _Project.Scripts.GameScene.GameLevel;
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
        [Inject] private IGameLevelController _gameLevelController;

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