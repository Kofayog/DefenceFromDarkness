using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerField : MonoBehaviour, IPoolable
{
    public RunesPool runesPool;
    public Transform m_Transform { get; private set; }

    public List<Rune> activeRunes = new List<Rune>();
    public int numberOfRunes = 3;
    public TriggerFieldsPool Pool { get; set; }


    public void Activate()
    {
        gameObject.SetActive(true);
        SpawnRunes(numberOfRunes);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        m_Transform = GetComponent<Transform>();
        gameObject.SetActive(false);
    }

    public void SpawnRunes(int numberOfRunes)
    {
        Rune rune;
        Vector3 prevPosition = Vector3.zero;

        for (int i = 0; i < numberOfRunes; i++)
        {
            rune = runesPool.GetObjectFromPool();
            Vector3 scale = rune.transform.localScale;

            rune.transform.parent = null;
            rune.transform.localScale = scale;
            rune.transform.SetParent(m_Transform, true);

            Vector3 horizontal = Vector3.zero;

            horizontal.x = Random.Range(m_Transform.position.x + 20, m_Transform.position.x - 30);
            horizontal.y = Random.Range(m_Transform.position.y + 16, m_Transform.position.y + 30);
            horizontal.z = Random.Range(m_Transform.position.z + 15, m_Transform.position.z - 20);
            
            Vector3 position = m_Transform.position + Vector3.up * horizontal.y;
            position = new Vector3(m_Transform.position.x + horizontal.x, horizontal.y, m_Transform.position.z + horizontal.z);

            rune.transform.position = horizontal;

            prevPosition = position;

            rune.Activate();
            rune.Pool = runesPool;
            rune.RuneDeactivated += RemoveRune;

            activeRunes.Add(rune);
        }
    }

    private void RemoveRune(Rune disabledRune)
    {
        disabledRune.RuneDeactivated -= RemoveRune;
        activeRunes.Remove(disabledRune);

        if (activeRunes.Count == 0)
        {
            Pool.ReturnObjectToPool(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out CannonBallControl cannonBall))
        {
            Debug.Log("Field Triggered");
            for (int i = 0; i < activeRunes.Count; i++)
            {
                activeRunes[i].Cast(cannonBall.cannon.gameObject);
            }
        }

        Debug.Log("Trigger Object" + other.attachedRigidbody.name);
    }
}
