using Enemies.States;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TargetAltar : MonoBehaviour, IHealth
{
    public event Action Destroyed;
    public event Action<int> Hit;

    #region UI 
    public CanvasGroup altarInfo;
    [SerializeField] private Image[] healthBarParts;

    public CanvasGroup respawnInfo;
    public Text respawnCountText;
    #endregion

    #region Health
    [SerializeField] private float maxHealth = 100;
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

            for (int i = 0; i < healthBarParts.Length; i++)
            {
                healthBarParts[i].fillAmount = health / maxHealth;
            }
        }
    }
    #endregion

    #region Timeline Directors 
    //public PlayableDirector attackDirector;
    public PlayableDirector spawnDirector;
    public PlayableDirector destroyDirector;
    #endregion

    #region AttackProperties
    [SerializeField] private float delayBetweenShots;
    [SerializeField] private EnemyMissile attackProjectile;

    public IntVariable numberOfShots;
    private WaitForSeconds waitBetweenShots;
    #endregion

    #region Collision variables
    private Vector3 localContactPoint;
    private Vector2 position;

    private Vector3 localMax;
    private Vector3 localMin;
    #endregion

    #region States

    public TargetStatesDictionary states = new TargetStatesDictionary();

    private EnemyState currentState;
    #endregion

    [SerializeField] private Transform m_transform;
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();

    public Transform m_Transform => m_transform;
    public Bounds BoundsInLocalSpace { get; private set; }

    public RunesPool circlesPool;

    public List<Rune> activePlatforms = new List<Rune>();
    public int numberOfPlatforms;

    void Start()
    {
        BoundsInLocalSpace = GetComponent<MeshFilter>().mesh.bounds;

        waitBetweenShots = new WaitForSeconds(delayBetweenShots);

        spawnDirector.RebuildGraph();
        destroyDirector.RebuildGraph();

        foreach(KeyValuePair<string, EnemyState> state in states)
        {
            //state.Value.Init(this);
        }

       
    }

    public void ChangeState (string stateID)
    {
        if (states.TryGetValue(stateID, out EnemyState state))
        {
            if (currentState != state)
            {
                currentState?.Exit();
                currentState = state;
                currentState.Enter();
            }
        }
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    localContactPoint = transform.InverseTransformPoint(collision.contacts[0].point);

    //    Debug.Log("LocalContact Target" + collision.contacts[0].point);

    //    position.x = (Mathf.Abs(localContactPoint.x) - bounds.max.x) / (bounds.center.x - bounds.max.x);
    //    position.y = (Mathf.Abs(localContactPoint.z) - bounds.max.z) / (bounds.center.z - bounds.max.z);

    //    var damageDealt = Mathf.RoundToInt(MaxHealth * position.x * position.y);

    //    RecieveDamage(damageDealt);

    //    Debug.Log("earnedPoints " + damageDealt);


    //}
    
    public void LaunchProjectiles(Transform target)
    {
        StartCoroutine(ShootProjectiles(target));
    }
    private IEnumerator ShootProjectiles(Transform target)
    {
        int count = 0;
        while (count < numberOfShots.RuntimeValue)
        {
            count++;

            var obj = Instantiate(attackProjectile) as EnemyMissile;
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.LookRotation(transform.up, Vector3.up);
            obj.Target = target;

            yield return waitBetweenShots;
        }
    }

    public void RecieveDamage(float damageAmount)
    {
        Health -= damageAmount;
        Debug.Log(name + " current health: " + Health);

        Hit?.Invoke(Mathf.RoundToInt(damageAmount));

        if (Health <= 0)
        {
            ChangeState("Destroyed");
        }
    }

    public void RestoreHealth(float healthAmount)
    {
        Health += healthAmount;
    }
    public void Spawn()
    {
        int randomPosition = Random.Range(0, spawnPositions.Count);

        m_Transform.position = spawnPositions[randomPosition].position;

        Rune platform;
        Vector3 prevPosition = Vector3.zero;
        
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            platform = circlesPool.GetObjectFromPool();

            Vector3 horizontal = Vector3.zero;
            horizontal.x = Random.Range(-10, 10);
            horizontal.z = Random.Range(-10, 10);

            Vector3 position = m_transform.position + Vector3.up * (prevPosition.y + 3f);
            position = new Vector3(m_transform.position.x + horizontal.x, position.y, m_transform.position.z + horizontal.z);

            platform.transform.position = position;

            prevPosition = position;

            //platform.Activate();
            activePlatforms.Add(platform);
        }

        spawnDirector.Play();
        Health = MaxHealth;

        ChangeState("Alive");
    }


    public void OnDestroyed()
    {
        Destroyed?.Invoke();
    }

}
