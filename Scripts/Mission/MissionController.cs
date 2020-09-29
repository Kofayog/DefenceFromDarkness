using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    [SerializeField] private Canvas CompleteScreen;
    [SerializeField] private Canvas FailedScreen;

    [SerializeField] private Text destroyedAmountText;
    [SerializeField] private Text requiredAmountText;

    [SerializeField] private TargetAltar target;
    [SerializeField] private Cannon cannon;

    [SerializeField] private int requiredAltars;

    private int destroyedAltars;
    public int DestroyedAltars
    {
        get
        {
            return destroyedAltars;
        }
        set
        {
            destroyedAltars = value;
            destroyedAmountText.text = destroyedAltars.ToString();
        }
    }

    private void OnEnable()
    {
        //target.Destroyed += AltarDestroyed;
        cannon.Destroyed += MissionFailed;
    }
    private void OnDisable()
    {
        //target.Destroyed -= AltarDestroyed;
        cannon.Destroyed -= MissionFailed;
    }

    private void Start()
    {
        requiredAmountText.text = requiredAltars.ToString();
    }

    public void SlowDownTime(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    public void AltarDestroyed()
    {
        DestroyedAltars++;
    }

    private void RestartMission()
    {

    }
    private void MissionFailed()
    {
        CompleteScreen.enabled = true;

        //FailedScreen.enabled = true;
    }
    private void MissionComplete()
    {
        
    }
}
