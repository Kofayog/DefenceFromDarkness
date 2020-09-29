using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class EnemyMissile : MonoBehaviour
{
    [SerializeField] ParticleSystem HitParticles;

    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float angularSpeed;
    [SerializeField] protected float speed;

    protected Vector3 direction;
    protected Quaternion targetRotation;
    protected float step;

    public Transform Target { get; set; }

    protected abstract void OnHit(GameObject target, Vector3 contactPoint);
    public virtual void FollowTarget()
    {
        Vector3 heading = Target.position - transform.position;
        float distance = heading.magnitude;
        direction = heading / distance;

        targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        step = Time.deltaTime * angularSpeed;


        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);

        transform.Translate(transform.forward * speed, Space.World);
    }

}
