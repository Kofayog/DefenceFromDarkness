using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableFactory/PathVisualizerFactory")]
public class PathVisualizerFactory : ScriptableFactory<PathVisualizer>
{
    public PathVisualizer visualizer;
    public override PathVisualizer ProducedObject => visualizer;

    public override PathVisualizer CreateInstance()
    {
        var visualizer = Instantiate(ProducedObject) as PathVisualizer;
        return visualizer;
    }
}
