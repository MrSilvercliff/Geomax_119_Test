using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Log;
using ZerglingUnityPlugins.Tools.Scripts.Repositories;

namespace Plugins.ZerglingUnityPlugins.Tools.Scripts.Repositories
{
    public interface IRepositoryDictionary<TKey, TItem> : IRepositoryBase<TItem>
    {
        void Add(TKey key, TItem item);
        void Remove(TKey key);
        void Remove(TItem item);
        bool TryGet(TKey key, out TItem item);
    }

    public abstract class RepositoryDictionary<TKey, TItem> : IRepositoryDictionary<TKey, TItem>
    {
        public int Count => _itemsDictionary.Count;

        protected Dictionary<TKey, TItem> _itemsDictionary;

        public RepositoryDictionary()
        {
            _itemsDictionary = new Dictionary<TKey, TItem>();
        }

        public abstract void Add(TItem item);

        public abstract void Add(TKey key, TItem item);

        public abstract void Remove(TKey key);

        public abstract void Remove(TItem item);

        public virtual void Clear()
        {
            _itemsDictionary.Clear();
        }

        public bool TryGet(TKey key, out TItem item)
        {
            var result = _itemsDictionary.TryGetValue(key, out item);

            if (!result)
                LogUtils.Error(this, $"Item with key [{key}] does not exist!");

            return result;
        }

        public IReadOnlyCollection<TItem> GetAll()
        {
            return _itemsDictionary.Values.ToArray();
        }
    }
}
