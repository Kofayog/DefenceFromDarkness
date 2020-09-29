using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericScriptablePool<T> : ScriptableObject where T: IPoolable
{
    public abstract ScriptableFactory<T> Factory { get; }
    private Queue<T> pool = new Queue<T>();

    public T GetObjectFromPool()
    {
        if (pool.Count == 0)
            AddObjectToPool();

        return pool.Dequeue();
    }

    public void ReturnObjectToPool(T objectToReturn)
    {
        objectToReturn.Deactivate();
        pool.Enqueue(objectToReturn);
    }

    public void AddObjectToPool()
    {
        var newObject = Factory.CreateInstance();
        newObject.Initialize();

        pool.Enqueue(newObject);
        
        
    }
}
