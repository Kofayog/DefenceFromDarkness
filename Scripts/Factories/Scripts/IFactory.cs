using UnityEngine;

public interface IFactory<T> where T: IPoolable
{
    T ProducedObject { get; }
    T CreateInstance();
}
