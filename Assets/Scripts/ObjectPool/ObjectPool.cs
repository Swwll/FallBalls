using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : PooledObject
{
    private readonly T _prefab;
    private readonly List<T> _pooledObjects = new List<T>();

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
    }

    public T GetObject()
    {
        if (_pooledObjects.Count > 0)
        {
            var pooledObject = _pooledObjects[0];
            _pooledObjects.RemoveAt(0);
            return pooledObject;
        }

        return GameObject.Instantiate(_prefab);
    }

    public void ReturnObject(T pooledObject)
    {
        _pooledObjects.Add(pooledObject);
    }
}