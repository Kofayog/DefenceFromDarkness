using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityResourceBar : MonoBehaviour
{
    public FloatVariable abilityResource;
    public Image resourceBar;

    Material material;
    private void OnEnable()
    {
        abilityResource.OnRuntimeValueChanged += UpdateBar;
    }
    private void OnDisable()
    {
        abilityResource.OnRuntimeValueChanged -= UpdateBar;
    }

    private void Start()
    {
        material = resourceBar.material;
    }
    public void UpdateBar(float value)
    {

    }
}
