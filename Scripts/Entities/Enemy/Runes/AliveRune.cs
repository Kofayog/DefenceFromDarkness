using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveRune : Rune
{
    public override void Activate()
    {
        gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
        OnRuneDeactivated();
    }

    public override void Initialize()
    {
        m_Transform = GetComponent<Transform>();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Pool.ReturnObjectToPool(this);
    }

}
