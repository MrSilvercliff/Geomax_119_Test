using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Repositories;

namespace Plugins.ZerglingUnityPlugins.Tools.Scripts.Repositories
{
    public interface IRepositoryList<TItem> : IRepositoryBase<TItem>
    {
        void Remove(TItem item);
        void Remove(int itemIndex);
        int IndexOf(TItem item);
    }

    public abstract class RepositoryList<TItem> : IRepositoryList<TItem>
    {
        public int Count => _items.Count;

        private List<TItem> _items;
        
        public RepositoryList()
        {
            _items = new();
        }

        public void Add(TItem item)
        {
            _items.Add(item);
        }
        
        public void Remove(TItem item)
        {
            _items.Remove(item);
        }

        public void Remove(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= _items.Count)
                return;
            
            _items.RemoveAt(itemIndex);
        }

        public int IndexOf(TItem item)
        {
            return _items.IndexOf(item);
        }

        public IReadOnlyCollection<TItem> GetAll()
        {
            return _items;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
