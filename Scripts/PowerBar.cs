using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public float MinValue
    {
        get
        {
            return powerBarSlider.minValue;
        }
        set
        {
            powerBarSlider.minValue = value;
        }
    }
    public float MaxValue
    {
        get
        {
            return powerBarSlider.maxValue;
        }
        set
        {
            powerBarSlider.maxValue = value;
        }
    }
    public float Value
    {
        get
        {
            return powerBarSlider.value;
        }
        set
        {
            LaunchPower.RuntimeValue = value;
            powerBarSlider.value = value;
        }
    }

    [SerializeField] private FloatVariable LaunchPower;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider powerBarSlider;
    [SerializeField] private Image powerFill;
    [SerializeField] private Gradient gradient;
    [SerializeField] private float fillSpeed;

    private bool isPowerBarActive = false;
    private float step;
    private float time;

    public void Activate()
    {
        time = 0f;
        step = 0f;
        Value = MinValue;
        isPowerBarActive = true;

        canvas.enabled = true;
    }
    public void Deactivate()
    {
        canvas.enabled = false;
        isPowerBarActive = false;
    }


    private void Update()
    {
        if (isPowerBarActive)
        {
            time += Time.deltaTime;
            step = Mathf.PingPong(time * fillSpeed, 1);
            Debug.Log("Bouncing");
            Value = Mathf.Lerp(MinValue, MaxValue, step);
            powerFill.color = gradient.Evaluate(step);
        }
    }
    //private WaitForEndOfFrame wait = new WaitForEndOfFrame();
    //private IEnumerator Bouncing()
    //{
    //    isPowerBarActive = true;
    //    float step = 0f;

    //    while (isPowerBarActive)
    //    {
    //        step = Mathf.PingPong(Time.unscaledTime * bounceSpeed, 1);
    //        Debug.Log("Bouncing");
    //        Value = Mathf.Lerp(MinValue, MaxValue, step);
    //        powerFill.color = gradient.Evaluate(step);

    //        yield return wait;
    //    }

    //    Debug.Log("Stop Bouncing");
    //}
    
}
