using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonBallStates
{
    Land,
    Launch,
    Fly,
    Fall
}
public abstract class CannonBallState: ScriptableObject
{
    public abstract CannonBallControl CannonBall { get; set; }

    public virtual void Initialize(CannonBallControl cannonBall)
    {
        CannonBall = cannonBall;

        Debug.Log("StateInitialized");
    }

    public virtual void Control(Vector3 direction)
    {

    }

    public abstract void Enter();
    public abstract void Exit();

}
