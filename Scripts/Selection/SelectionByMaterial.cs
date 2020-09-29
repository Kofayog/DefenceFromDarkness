using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionByMaterial : SelectionObject
{
    [SerializeField] private Material selectMaterial;

    private Material defaultMaterial;

    private Renderer m_renderer;

    private void Start()
    {
        m_renderer = GetComponent<Renderer>();
        defaultMaterial = m_renderer.material;
    }
    public override void Select()
    {
        m_renderer.material = selectMaterial;
    }
    public override void Deselect()
    {
        m_renderer.material = defaultMaterial;
    }

}
