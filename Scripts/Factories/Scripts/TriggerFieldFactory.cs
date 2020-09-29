using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableFactory/TriggerFieldFactory")]
public class TriggerFieldFactory : ScriptableFactory<TriggerField>
{
    [SerializeField] private TriggerField prefab;
    public override TriggerField ProducedObject => prefab;

    public override TriggerField CreateInstance()
    {
        var rune = Instantiate(ProducedObject) as TriggerField;
        return rune;
    }
}
