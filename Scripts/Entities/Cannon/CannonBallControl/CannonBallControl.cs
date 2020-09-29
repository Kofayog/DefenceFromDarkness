using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using DG.Tweening;
using UnityEngine.Playables;
using UnityEngine.UI;
using Enemies;


[System.Serializable]
public struct CannonBallUI
{
    public Canvas canvas;
    public Text cooldownText;
    public GameObject JoystickButton;
    public GameObject DisableFlyButton;
}
public class CannonBallControl : ObjectControl
{
    public Rigidbody m_rigidbody { get; private set; }
    public Transform m_transform { get; private set; }
    public bool canMove { get; set; } = false;
    public float Damage { get; private set; }
    /* Future UI Module */
    [SerializeField] public Transform projectile;
    [SerializeField] private FloatVariable abilityResource;
    [SerializeField] private CannonBallAbility ability;

    public CannonBallUI cannonBallUI;

    public CarrierController carrier;
    public TimeManager timeManager;
    public ClickDetector clickDetector;

    public CinemachineFreeLook freeLookCamera;

    [SerializeField] private PlayableDirector missHitPlayable;
    [SerializeField] private PlayableDirector hitTargetPlayable;
    [SerializeField] private float returnToCannonDelay = 2f;

    [HideInInspector] public Vector3 moveVector = new Vector3();
    private WaitForSeconds waitForSeconds;

    private float previousHeight;
    private bool isGrounded;

    public CannonBallStatesDictionary states = new CannonBallStatesDictionary();

    private CannonBallState currentState;


    Vector3 localContactPoint;
    Vector3 position;
    public Tweener projectileTweener;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();

        waitForSeconds = new WaitForSeconds(returnToCannonDelay);

        foreach (KeyValuePair<CannonBallStates, CannonBallState> state in states)
        {
            state.Value.Initialize(this);
        }

        projectileTweener = projectile.GetChild(0).DOLocalRotate(new Vector3(360f, 0, 0), 0.35f, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear).SetAutoKill(false);

        StopRotation();

        missHitPlayable.RebuildGraph();
        hitTargetPlayable.RebuildGraph();

        ChangeState(CannonBallStates.Land);
    }

    public void CameraUpdateType(CinemachineBrain.UpdateMethod updateMethod)
    {
        cannon.cameraBrain.m_UpdateMethod = updateMethod;
    }
    public override void Activate()
    {
        CameraUpdateType(CinemachineBrain.UpdateMethod.FixedUpdate);
        StartCoroutine(GainAltitude());

        isGrounded = false;

        freeLookCamera.VirtualCameraGameObject.SetActive(true);
        

        cannonBallUI.canvas.enabled = true;
        m_rigidbody.useGravity = true;
        m_rigidbody.detectCollisions = true;
    }
    public override void Deactivate()
    {
        CameraUpdateType(CinemachineBrain.UpdateMethod.SmartUpdate);
        freeLookCamera.VirtualCameraGameObject.SetActive(false);
        
        m_rigidbody.useGravity = false;
        m_rigidbody.drag = 0;
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.detectCollisions = false;
        canMove = false;



        cannonBallUI.canvas.enabled = false;

    }

    public void ChangeState(CannonBallStates stateID)
    {
        if (states.TryGetValue(stateID, out CannonBallState state))
        {
            if (currentState != state)
            {
                currentState?.Exit();
                currentState = state;
                currentState.Enter();
            }
        }
        
    }
    public void ChangeState(CannonBallState state)
    {
        if (currentState != state)
        {
            if (state.CannonBall == null)
                state.Initialize(this);

            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }
    }

    public void StartRotation()
    {
        projectileTweener.Play();
    }
    public void StopRotation()
    {
        projectileTweener.Pause();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //StopCoroutine(Glide());
        //StartCoroutine(Glide());

        //projectile.DOPause();

        if (collision.collider.TryGetComponent(out DarkAltar target))
        {
            localContactPoint = target.m_Transform.InverseTransformPoint(collision.contacts[0].point);

            position.x = (Mathf.Abs(localContactPoint.x) - target.BoundsInLocalSpace.max.x) / (target.BoundsInLocalSpace.center.x - target.BoundsInLocalSpace.max.x);
            position.y = (Mathf.Abs(localContactPoint.z) - target.BoundsInLocalSpace.max.z) / (target.BoundsInLocalSpace.center.z - target.BoundsInLocalSpace.max.z);

            var damageDealt = Mathf.RoundToInt(target.MaxHealth * position.x * position.y);

            target.RecieveDamage(damageDealt);

            if (target.Health > 0)
                StartCoroutine(ReturnToCannon());

            ChangeState(CannonBallStates.Land);
        }
        else
        {
            
        }

        if (!collision.collider.CompareTag("Target"))
        {
            projectile.DOPause();

            ChangeState(CannonBallStates.Land);

            Debug.Log("Missed");
            missHitPlayable.Play();
            cannon.OnMissTarget();
        }
        //Deactivate();

        //if (collision.collider.tag == "Target")
        //{
        //    hitTargetPlayable.Play();
        //}
        //else
        //{

        //}
    }
    private void OnTriggerExit(Collider other)
    {
        cannon.Control = cannon.cannon;

        Debug.Log("CannonBall leave Location");
    }

    public override void Control(Vector3 direction)
    {
        moveVector.Set(direction.x, 0, direction.y);
        currentState.Control(moveVector); 
    }

    public void DisableFlyMode()
    {
        ChangeState(CannonBallStates.Fall);
        
    }
    public void CastAbility()
    {
        //ChangeState("TimeStop");

        if (abilityResource.RuntimeValue >= ability.cost)
        {
            ability.Cast(this);
            abilityResource.RuntimeValue -= ability.cost;
        }
        
    }

    private IEnumerator ReturnToCannon()
    {
        yield return waitForSeconds;

        Debug.Log("ReturnToCannon");
        cannon.Control = cannon.cannon;
        cannon.OnReturnToCannon();
    }
    private IEnumerator GainAltitude()
    {
        previousHeight = m_transform.position.y;

        ChangeState(CannonBallStates.Launch);

        while (m_transform.position.y >= previousHeight)
        {
            previousHeight = m_transform.position.y;

            yield return null;
        }

        

        ChangeState(CannonBallStates.Fly);
    }

}
