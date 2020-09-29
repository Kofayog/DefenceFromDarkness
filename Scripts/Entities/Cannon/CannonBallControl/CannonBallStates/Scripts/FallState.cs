using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CannonBallState/Fall")]
public class FallState : CannonBallState
{

    public override CannonBallControl CannonBall { get; set; }

    public override void Enter()
    {
        //CannonBall.StartRotation();
        CannonBall.cannonBallUI.JoystickButton.SetActive(true);
        CannonBall.m_rigidbody.drag = 0f;
        Debug.Log("Ball Falling");
    }

    public override void Exit()
    {
        CannonBall.cannonBallUI.JoystickButton.SetActive(false);
    }

    public override void Control(Vector3 direction)
    {
        
        
        //CannonBall.m_rigidbody.AddRelativeForce(direction * Time.deltaTime * CannonBall.ForcePower, ForceMode.VelocityChange);

        //direction *= CannonBall.ForcePower * Time.deltaTime;
        //direction.y = CannonBall.m_rigidbody.velocity.y;
        //CannonBall.m_rigidbody.velocity = direction;
    }
}
