using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableFactory<T> : ScriptableObject where T: IPoolable
{
    public abstract T ProducedObject { get; }
    public abstract T CreateInstance();

}
