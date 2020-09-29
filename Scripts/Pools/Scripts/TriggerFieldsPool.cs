using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptablePool/TriggerFieldsPool")]
public class TriggerFieldsPool : GenericScriptablePool<TriggerField>
{
    [SerializeField] private TriggerFieldFactory factory;
    public override ScriptableFactory<TriggerField> Factory => factory;

    
}
