using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody m_rigidbody;


    [SerializeField] private Vector2 axis;
    [SerializeField] private float yOffset;

    [SerializeField] private float orbitPeriod;

    private Transform m_transform;
    private float rotationSpeed = 20f;
    private float step;

    private Vector3 targetPosition;
    private Vector3 direction;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
    }


    //private void FixedUpdate()
    //{
    //    rotationSpeed = 1f / orbitPeriod;
    //    step += Time.deltaTime * rotationSpeed;
    //    step %= 1f;
    //    evaluate = Evaluate(step);

    //    targetPosition.Set(evaluate.x, yOffset, evaluate.y);

    //    direction = targetPosition - m_transform.position;

    //    m_rigidbody.velocity = direction;

    //}

    private void Update()
    {
        rotationSpeed = 1f / orbitPeriod;
        step += Time.deltaTime * rotationSpeed;
        step %= 1f;
        evaluate = Evaluate(step);

        targetPosition.Set(evaluate.x, yOffset, evaluate.y);

        direction = targetPosition - m_transform.position;
        m_rigidbody.velocity = direction;

        m_transform.LookAt(target);
    }

    float angle;
    Vector2 evaluate;
    public Vector2 Evaluate(float t)
    {
        angle = Mathf.Deg2Rad * 360f * t;
        evaluate.x = target.position.x + Mathf.Sin(angle) * axis.x;
        evaluate.y = target.position.z + Mathf.Cos(angle) * axis.y;

        return evaluate;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        float theta = 0;
        Vector2 position = Evaluate(theta);
        Vector3 pos = new Vector3(position.x, yOffset, position.y);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.01f; theta < Mathf.PI * 2; theta += 0.01f)
        {
            position = Evaluate(theta);
            newPos = new Vector3(position.x, yOffset, position.y);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
    }
#endif
}
