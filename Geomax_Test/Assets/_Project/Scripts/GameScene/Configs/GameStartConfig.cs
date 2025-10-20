using UnityEngine;

namespace _Project.Scripts.GameScene.Configs
{
    public interface IGameStartConfig
    { 
        string PlayerCreatureId { get; }
        string[] EnemyCreatureIds { get; }
    }

    [CreateAssetMenu(fileName = "GameStartConfig", menuName = "Project/Configs/Game Scene/Game Start Config")]
    public class GameStartConfig : ScriptableObject, IGameStartConfig
    {
        public string PlayerCreatureId => _playerCreatureId;
        public string[] EnemyCreatureIds => _enemyCreatureIds;

        [SerializeField] private string _playerCreatureId;
        [SerializeField] private string[] _enemyCreatureIds;
    }
}