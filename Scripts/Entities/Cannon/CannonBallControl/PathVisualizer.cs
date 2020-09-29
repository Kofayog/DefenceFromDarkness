using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PathVisualizer : MonoBehaviour, IPoolable, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public event System.Action<Vector3> ReleasePointer;

    public LineRenderer pathRenderer;
    public Image pathHint;
    public LayerMask layerMask;

    public float PathRange { get; set; }
    public Transform ballTransform { get; set; }

    private Camera m_camera;
    private Ray ray;

    public void Activate()
    {
        gameObject.SetActive(true);

        pathHint.rectTransform.position = m_camera.WorldToScreenPoint(ballTransform.position);
    }

    public void Deactivate()
    {
        pathHint.enabled = true;
        pathRenderer.enabled = false;
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        m_camera = Camera.main;
        pathRenderer.useWorldSpace = true;

        gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");

        
        pathRenderer.SetPosition(0, ballTransform.position);
        pathRenderer.SetPosition(1, GetPoint(eventData.position, PathRange));
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // Disable click hint
        pathHint.enabled = false;
        pathRenderer.enabled = true;

        pathRenderer.SetPosition(0, ballTransform.position);
        pathRenderer.SetPosition(1, GetPoint(eventData.position, PathRange));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ReleasePointer?.Invoke(GetPoint(eventData.position, PathRange));
    }

    public Vector3 GetPoint(Vector3 position, float Range)
    {
        ray = m_camera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out RaycastHit hit, Range, layerMask))
            return hit.point;
        else
            return ray.GetPoint(Range);
    }
}
