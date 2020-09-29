using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptablePool/PathVisualizerPool")]
public class PathVisualizerPool : GenericScriptablePool<PathVisualizer>
{
    public PathVisualizerFactory factory;
    public override ScriptableFactory<PathVisualizer> Factory => factory;

    
}
