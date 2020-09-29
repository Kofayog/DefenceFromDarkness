using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableFactory/ProjectileFactory")]
public class ProjectileFactory : ScriptableFactory<ProjectileBehaviour>
{
    public ProjectileBehaviour prefab;
    public override ProjectileBehaviour ProducedObject => prefab;

    public override ProjectileBehaviour CreateInstance()
    {
        var rune = Instantiate(ProducedObject) as ProjectileBehaviour;
        return rune;
    }

    
}
