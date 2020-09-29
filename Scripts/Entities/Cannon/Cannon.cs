using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Cinemachine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour, IHealth
{
    public event System.Action MissTarget;
    public event System.Action ReturnToCannon;
    public event System.Action HealthChanged;
    public event System.Action Destroyed;

    public ObjectControl cannon;
    public ObjectControl cannonBall;

    public CinemachineBrain cameraBrain;

    [SerializeField] private Image healthBar;

    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    public float MaxHealth => maxHealth;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            healthBar.fillAmount = health / maxHealth;
        }
    }

    [SerializeField] private ObjectControl defaultControl;
    public ObjectControl Control
    {
        get
        {
            return defaultControl;
        }
        set
        {
            defaultControl.Deactivate();
            defaultControl = value;
            defaultControl.Activate();
        }
    }

    private Vector3 direction = new Vector3();

    private void Start()
    {
        cannon.Init(this);
        cannonBall.Init(this);

        Control = defaultControl;
    }

    void Update()
    {
        direction.x = CrossPlatformInputManager.GetAxis("Horizontal");
        direction.y = CrossPlatformInputManager.GetAxis("Vertical");


        Control.Control(direction);

    }

    public void OnMissTarget()
    {
        MissTarget?.Invoke();
    }
    public void OnReturnToCannon()
    {
        ReturnToCannon?.Invoke();
    }
    public void RecieveDamage(float damageAmount)
    {
        Health -= damageAmount;

        if (Health <= 0)
        {
            Destroyed?.Invoke();
        }
    }

    public void RestoreHealth(float healthAmount)
    {

    }
}
