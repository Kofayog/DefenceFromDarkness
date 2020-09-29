using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CannonBallState/TimeStopState")]
public class TimeStopState : CannonBallState
{
    public CannonBallState nextState;
    public override CannonBallControl CannonBall { get; set; }

    public PathVisualizerPool visualizerPool;

    public FloatVariable timeToExit;
    public Vector3Variable targetPoint;

    public float range;

    private WaitForSecondsRealtime waitForSecond = new WaitForSecondsRealtime(1);
    private PathVisualizer visualizer;

    private Coroutine exitTimer;

    public override void Enter()
    {
        visualizer = visualizerPool.GetObjectFromPool();
        visualizer.ballTransform = CannonBall.m_transform;
        visualizer.PathRange = range;
        visualizer.Activate();

        visualizer.ReleasePointer += TriggerDash;

        CannonBall.CameraUpdateType(Cinemachine.CinemachineBrain.UpdateMethod.LateUpdate);

        CannonBall.timeManager.SlowdownTime(.05f);
        CannonBall.m_rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        exitTimer = CannonBall.StartCoroutine(ExitTimer());
        Debug.Log("Enter JumpDash");
    }

    public override void Exit()
    {
        visualizer.ReleasePointer -= TriggerDash;

        CannonBall.timeManager.RestoreRealTime(1);
        
        visualizerPool.ReturnObjectToPool(visualizer);

        Debug.Log("Exit JumpDash");
    }

    private IEnumerator ExitTimer()
    {
        float timer = timeToExit.RuntimeValue;

        do
        {
            CannonBall.cannonBallUI.cooldownText.text = timer.ToString();
            timer--;
            yield return waitForSecond;
        }
        while (timer > 0);

        CannonBall.cannonBallUI.cooldownText.text = "";
        CannonBall.ChangeState(CannonBallStates.Fall);

    }

    private void TriggerDash(Vector3 targetPosition)
    {
        CannonBall.StopCoroutine(exitTimer);

        targetPoint.RuntimeValue = targetPosition;
        CannonBall.cannonBallUI.cooldownText.text = "";
        CannonBall.ChangeState(nextState);
    }

    public override void Control(Vector3 direction)
    {

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.touches[0];



        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        time = Time.time;
        //    }

        //    if (touch.phase == TouchPhase.Ended)
        //    {
        //        //Debug.Log("Time.time " + Time.time + "time " + time + "Time.time - time = " + (Time.time - time));
        //        float diff = Time.time - time;


        //        if (diff < 0.02f)
        //        {
        //            targetPoint.RuntimeValue = CannonBall.clickDetector.GetPoint(range);
        //            visualizer.Activate();

        //            isTrigger = true;
        //            visualizer.pathRenderer.SetPosition(0, CannonBall.m_transform.position);
        //            visualizer.pathRenderer.SetPosition(1, targetPoint.RuntimeValue);
        //        }

        //    }

        //    if (!isTrigger)
        //    {
        //        targetPoint.RuntimeValue = CannonBall.m_transform.position;
        //    }
        //}
        



        
    }
}
