using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AltarIdleAnimation : MonoBehaviour
{
    public Transform outerCircle;
    public Transform innerCircle;

    public Vector3 outerRotation;
    public Vector3 innerRotation;
    
    public float rotationTime;
    // Start is called before the first frame update
    void Start()
    {
        outerCircle.DORotate(outerRotation, rotationTime, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
        innerCircle.DORotate(innerRotation, rotationTime, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
    }

    
}
