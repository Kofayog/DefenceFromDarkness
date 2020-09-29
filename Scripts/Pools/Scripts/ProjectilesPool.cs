using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptablePool/ProjectilePool")]
public class ProjectilesPool : GenericScriptablePool<ProjectileBehaviour>
{
    public ProjectileFactory factory;
    public override ScriptableFactory<ProjectileBehaviour> Factory => factory;

    
}
