using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : SkillBehaviour, IPoolable
{
    [SerializeField] private ParticleSystem HitParticles;
    [SerializeField] private float angularSpeed;

    public ProjectilesPool Pool;
    public float Damage { get; set; }
    public float Speed { get; set; }


    protected Vector3 direction;
    protected Quaternion targetRotation;
    protected float step;

    public Transform m_Transform { get; private set; }
    public Transform Target { get; set; }

    private void OnEnable()
    {
        particleController.OnCollision += OnHit;
    }
    private void OnDisable()
    {
        particleController.OnCollision -= OnHit;
    }

    IHealth[] targetsHealth;
    protected void OnHit(GameObject target, Vector3 contactPoint)
    {
        Debug.Log("Hit" + target.name);

        targetsHealth = target.GetComponentsInChildren<IHealth>();

        if (targetsHealth.Length > 0)
        {
            targetsHealth[0].RecieveDamage(Damage);
        }

        Pool.ReturnObjectToPool(this);
    }

    private void FollowTarget()
    {
        Vector3 heading = Target.position - transform.position;
        float distance = heading.magnitude;
        direction = heading / distance;

        targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        step = Time.deltaTime * angularSpeed;


        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);

        transform.Translate(transform.forward * Speed, Space.World);
    }

    private void Update()
    {
        FollowTarget();
    }

    public void Initialize()
    {
        m_Transform = GetComponent<Transform>();
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
