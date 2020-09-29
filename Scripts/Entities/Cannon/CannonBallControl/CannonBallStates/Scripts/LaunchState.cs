using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CannonBallState/Launch")]
public class LaunchState : CannonBallState
{
    public override CannonBallControl CannonBall { get; set; }

    public override void Enter()
    {
        //CannonBall.StartRotation();
    }

    public override void Exit()
    {
        
    }
}
