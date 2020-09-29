using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLance : EnemyMissile
{
    [SerializeField] private ParticleController particleController;

    private void OnEnable()
    {
        particleController.OnCollision += OnHit;
    }
    private void OnDisable()
    {
        particleController.OnCollision -= OnHit;
    }
    protected override void OnHit(GameObject target, Vector3 contactPoint)
    {
        Debug.Log("Hit");

        if (target.TryGetComponent(out IHealth health))
            health.RecieveDamage(damage);

        Destroy(gameObject);
    }

    void Update()
    {
        FollowTarget();
    }
}
