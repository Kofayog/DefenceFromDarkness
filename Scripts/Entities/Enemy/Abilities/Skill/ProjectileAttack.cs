using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Skills/ProjectileAttack")]
public class ProjectileAttack : Skill
{
    public ProjectilesPool projectilesPool;

    public float damage;
    public float speed;

    public override void Cast(Transform origin, GameObject target)
    {
        Debug.Log("Pool name " + projectilesPool.name);
        var projectile = projectilesPool.GetObjectFromPool();

        projectile.Activate();
        projectile.m_Transform.position = origin.position;
        projectile.m_Transform.rotation = Quaternion.LookRotation(Vector3.up);

        projectile.Damage = damage;
        projectile.Speed = speed;
        projectile.Target = target.transform;
        Debug.Log("Skill Cast Performed");
    }

}
