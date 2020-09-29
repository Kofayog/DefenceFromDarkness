using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rune : MonoBehaviour, IPoolable
{
    public event System.Action<Rune> RuneDeactivated;
    public Skill skill;
    public RunesPool Pool { get; set; }
    public Transform m_Transform { get; protected set; }

    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void Initialize();

    public void OnRuneDeactivated()
    {
        RuneDeactivated?.Invoke(this);
    }

    public void Cast(GameObject target)
    {
        skill.Cast(m_Transform, target);
    }
}
