using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public event System.Action<GameObject, Vector3> OnCollision;

    public ParticleSystem VisualEffect { get; private set; }
    public List<ParticleCollisionEvent> collisionEvents;

    public ParticleSystemRenderer particleSystemRenderer { get; private set; }
    int numberOfCollisions;
    // Start is called before the first frame update
    void Awake()
    {
        VisualEffect = GetComponent<ParticleSystem>();
        particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    public void Activate()
    {
        VisualEffect.Play();
    }
    public void Deactivate()
    {
        VisualEffect.Stop();
    }

    public void OnParticleCollision(GameObject other)
    {
        if (VisualEffect != null)
            numberOfCollisions = VisualEffect.GetCollisionEvents(other, collisionEvents);
        OnCollision?.Invoke(other, collisionEvents[0].intersection);
    }
}
