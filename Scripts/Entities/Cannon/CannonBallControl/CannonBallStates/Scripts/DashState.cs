using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CannonBallState/Dash")]
public class DashState : CannonBallState
{
    public override CannonBallControl CannonBall { get; set; }

    public Vector3Variable targetPoint;
    public float DashPower;

    private float timeToStop;

    public override void Enter()
    {
        CannonBall.StartRotation();
        Debug.Log("DashEnter");
        CannonBall.StartCoroutine(CastDash());
        CannonBall.m_rigidbody.useGravity = false;
    }

    public override void Exit()
    {
        CannonBall.StopRotation();
        CannonBall.m_rigidbody.useGravity = true;
        
        Debug.Log("DashExit");
        CannonBall.m_rigidbody.velocity = CannonBall.m_rigidbody.velocity * 0.1f;

        CannonBall.m_rigidbody.interpolation = RigidbodyInterpolation.None;
        CannonBall.CameraUpdateType(Cinemachine.CinemachineBrain.UpdateMethod.FixedUpdate);
    }

    private IEnumerator CastDash()
    {
        var heading = targetPoint.RuntimeValue - CannonBall.m_transform.position;
        var distance = heading.magnitude;
        var direction = Vector3.zero;

        if (distance > 0)
            direction = heading / distance;

        CannonBall.projectile.transform.rotation = Quaternion.LookRotation(direction);

        CannonBall.m_rigidbody.velocity = direction * DashPower;
        timeToStop = distance / DashPower;


        while (timeToStop > 0)
        {
            timeToStop -= Time.deltaTime;

            yield return null;
        }

        CannonBall.ChangeState(CannonBallStates.Fall);
    }

}
