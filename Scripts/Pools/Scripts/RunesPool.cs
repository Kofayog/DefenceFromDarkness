using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptablePool/RunesPool")]
public class RunesPool : GenericScriptablePool<Rune>
{
    [SerializeField] private RuneFactory factory;
    public override ScriptableFactory<Rune> Factory => factory;


}
