using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync;

namespace _Project.Scripts.Project.Animations
{
    public interface IMonoBehaviourAnimatorController : IInitializable, IFlushable
    {
        int CurrentState { get; }

        void Play(int stateHash, int layer = 0, int normalizedTime = 0);
    }

    public class MonoBehaviourAnimatorController : MonoBehaviour, IMonoBehaviourAnimatorController
    {
        public int CurrentState => _currentStateHash;

        [SerializeField] private Animator _animator;

        private int _currentStateHash;

        public bool Init()
        {
            _currentStateHash = AnimatorStateHash.Empty;
            _animator.Play(_currentStateHash, 0);
            return true;
        }

        public bool Flush()
        {
            return true;
        }

        public void Play(int stateHash, int layer = 0, int normalizedTime = 0)
        {
            _currentStateHash = stateHash;
            _animator.Play(_currentStateHash, layer, normalizedTime);
        }
    }
}