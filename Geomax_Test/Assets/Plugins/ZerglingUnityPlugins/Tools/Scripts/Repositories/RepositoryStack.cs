using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZerglingUnityPlugins.Tools.Scripts.Repositories
{
    public interface IRepositoryStack<T> : IRepositoryBase<T>
    {
        T Get();
    }

    public abstract class RepositoryStack<T> : IRepositoryStack<T>
    {
        public int Count => _stack.Count;

        protected Stack<T> _stack;

        public void Add(T item)
        {
            _stack.Push(item);
        }

        public T Get()
        {
            return _stack.Pop();
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _stack.ToArray();
        }

        public void Clear()
        {
            while (_stack.Count > 0)
                _stack.Pop();
        }
    }
}


