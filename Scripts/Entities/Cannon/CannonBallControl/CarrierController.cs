using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarrierController : MonoBehaviour
{
    public Transform body;
    public Vector3 targetScale;

    public float elasticity;
    public float duration;
    public int virbrato;

    private Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = body.localScale;
    }

    public void Activate()
    {
        body.gameObject.SetActive(true);
        body.DOPunchScale(targetScale, duration, virbrato, elasticity);
    }
    public void Deactivate()
    {
        body.gameObject.SetActive(false);
        body.localScale = defaultScale;
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //        Activate();
    //    else if (Input.GetMouseButtonDown(1))
    //        Deactivate();
    //}
}
