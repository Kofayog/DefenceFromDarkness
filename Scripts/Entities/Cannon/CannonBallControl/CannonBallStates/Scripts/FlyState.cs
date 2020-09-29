using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CannonBallState/Fly")]
public class FlyState : CannonBallState
{
    public override CannonBallControl CannonBall { get; set; }

    public ParticleSystem Effect;
    public AudioSource audio;

    public float drag;
    public FloatVariable launchPower;

    public override void Enter()
    {
        CannonBall.m_transform.rotation = Quaternion.LookRotation(CannonBall.m_transform.forward - Vector3.Scale(CannonBall.m_transform.forward, Vector3.up), Vector3.up);
        CannonBall.carrier.Activate();
        CannonBall.cannonBallUI.DisableFlyButton.SetActive(true);

        Debug.Log("launchPower " + launchPower.RuntimeValue);
        CannonBall.m_rigidbody.drag = drag;
    }

    public override void Exit()
    {
        CannonBall.carrier.Deactivate();
        CannonBall.cannonBallUI.DisableFlyButton.SetActive(false);
        
        CannonBall.m_rigidbody.drag = 0f;

        //CannonBall.m_rigidbody.AddRelativeForce(CannonBall.m_transform.forward * flyingSpeed, ForceMode.Acceleration);

    }

    public override void Control(Vector3 direction)
    {
        CannonBall.m_rigidbody.AddForce(CannonBall.m_transform.forward * launchPower.RuntimeValue, ForceMode.Force);
    }
}
