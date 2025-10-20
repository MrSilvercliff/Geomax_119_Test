using _Project.Scripts.GameScene.GameLoop;
using _Project.Scripts.Project.Scenes;
using System.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneController : SceneController
    {
        [Inject] private IGameSceneServiceIniter _serviceIniter;
        [Inject] private IGameLoopController _gameLoopController;

        protected override async Task OnAwake()
        {
            await _serviceIniter.Init();

            await _gameLoopController.OnAwake();
        }

        protected override async Task OnStart()
        {
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