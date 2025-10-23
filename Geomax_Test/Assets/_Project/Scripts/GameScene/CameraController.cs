using UnityEngine;

namespace _Project.Scripts.GameScene
{
    public interface ICameraController
    { 
        Camera Camera { get; }
    }

    public class CameraController : MonoBehaviour, ICameraController
    {
        public Camera Camera => _camera;

        [SerializeField] private Camera _camera;
    }
}