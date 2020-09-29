using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CannonBallAbilities/Dash")]
public class DashAbility : CannonBallAbility
{
    public CannonBallState timeStopState;
    public override void Cast(CannonBallControl caster)
    {
        caster.ChangeState(timeStopState);
    }

   
}
