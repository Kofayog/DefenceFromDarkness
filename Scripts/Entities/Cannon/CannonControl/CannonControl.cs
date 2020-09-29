using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

[System.Serializable]
public struct CannonRotation
{
    public float min;
    public float max;
    public float power;
}
[System.Serializable]
public struct CannonPower
{
    public float minValue;
    public float maxValue;
    public float currentValue;

}
public class CannonControl : ObjectControl
{
    [SerializeField] private Transform platform;
    [SerializeField] private Transform barrel;
    [SerializeField] private Rigidbody cannonBall;

    [SerializeField] private CannonRotation cannonRotation;
    [SerializeField] private CannonPower cannonPower;

    /* Future UI Module */
    [SerializeField] private Canvas cannonUI;
    [SerializeField] private PowerBar powerBar;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private float rotationAngle;

    //private void OnEnable()
    //{
    //    powerSlider.onValueChanged.AddListener(OnPowerChanged);
    //}
    //private void OnDisable()
    //{
    //    powerSlider.onValueChanged.RemoveListener(OnPowerChanged);
    //}
    private void Start()
    {
        powerBar.MinValue = cannonPower.minValue;
        powerBar.MaxValue = cannonPower.maxValue;
    }

    public override void Activate()
    {
        virtualCamera.VirtualCameraGameObject.SetActive(true);
        cannon.cameraBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        cannonUI.enabled = true;
    }

    public override void Control(Vector3 direction)
    {
        rotationAngle += Time.deltaTime * direction.x * cannonRotation.power;

        if (rotationAngle >= cannonRotation.max)
            rotationAngle = cannonRotation.min;

        rotationAngle = Mathf.Clamp(rotationAngle, cannonRotation.min, cannonRotation.max);

        platform.localRotation = Quaternion.AngleAxis(rotationAngle, Vector3.up);
    }

    public override void Deactivate()
    {
        virtualCamera.VirtualCameraGameObject.SetActive(false);
        cannon.cameraBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
        cannonUI.enabled = false;
    }

    public void Launch()
    {
        cannonBall.transform.position = barrel.position;

        cannonBall.transform.rotation = barrel.rotation;
        cannon.Control = cannon.cannonBall;

        Vector3 direction = barrel.forward * powerBar.Value;
        cannonBall.AddForce(direction, ForceMode.Impulse);


        Debug.Log("Launched: " + powerBar.Value);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(barrel.position, (barrel.forward - Vector3.Scale(barrel.forward, Vector3.up)) * 1000);
    }
}
