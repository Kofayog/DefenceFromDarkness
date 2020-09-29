using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private Camera m_camera;

    private Ray ray;
    private Vector3 position;
    public float range = 20f;
    public float radius = 2f;
    public Transform ball;

    public bool DetectObject(LayerMask mask, out RaycastHit hit)
    {
        ray = m_camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, mask))
        {
            return true;
        }

        return false;
    }

    public Vector3 GetPoint(float Range)
    {
        ray = m_camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Range))
            return hit.point;
        else
            return ray.GetPoint(Range);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(ball.position, GetPoint(range));
        Gizmos.DrawWireSphere(GetPoint(range), radius);
    }
}
