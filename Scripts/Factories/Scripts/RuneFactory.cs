using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableFactory/RuneFactory")]
public class RuneFactory : ScriptableFactory<Rune>
{
    [SerializeField] private Rune prefab;
    public override Rune ProducedObject => prefab;

    public override Rune CreateInstance()
    {
        var rune = Instantiate(ProducedObject) as Rune;
        return rune;
    }


}
