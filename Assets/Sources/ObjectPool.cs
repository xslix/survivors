using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Survivors
{
    public interface IObjectPool<T> where T : MonoBehaviour
    {
        public T Get();
        public void Release(T obj);
    }
    public class ObjectPool<T> : IObjectPool<T> where T : MonoBehaviour
    {
        private List<T> _pool = new();
        private T _prefab;


        public ObjectPool(T prefab)
        {
            _prefab = prefab;
        }

        public T Get()
        {
            T obj;
            if (_pool.IsEmpty())
            {
                obj = Object.Instantiate(_prefab);

            }
            else
            {
                obj = _pool[^1];
                _pool.RemoveAt(_pool.Count - 1);
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Add(obj);
        }
    }
}