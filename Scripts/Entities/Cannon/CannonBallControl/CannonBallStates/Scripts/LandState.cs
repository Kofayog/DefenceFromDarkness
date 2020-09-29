using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CannonBallState/Land")]
public class LandState : CannonBallState
{
    public override CannonBallControl CannonBall { get; set; }

    public override void Enter()
    {
        Debug.Log("Ball Landed");
        //CannonBall.StopRotation();
    }

    public override void Exit()
    {
        
    }


    /* Effects */

    /* Audio */

}
