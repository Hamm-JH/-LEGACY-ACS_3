using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [Header("HUD status")]
    public F16HUDManager HUDManager;

    [Header("input check")]
    public ControllerInputCheck inputcheck;

    [Header ("AudioController")]
    public AudioController audioController;

    [Header("private values")]
    private float time;
    private bool shotCheck;
    private F16HUDManager.HUDState HUDstat;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        shotCheck = false;
        HUDstat = F16HUDManager.HUDState.FlightState;
    }

    // Update is called once per frame
    void Update()
    {
        HUDstat = HUDManager.stateHUD;

        switch(HUDstat)
        {
            case F16HUDManager.HUDState.FlightState:

                break;

            case F16HUDManager.HUDState.FightState:
                controlCheck();
                break;
        }
    }

    private void controlCheck()
    {
        if(inputcheck.LTriggerClicked.stateDown)
        {
            audioController.vulkanReady.Play();
        }
        else if(inputcheck.LTriggerClicked.state)
        {
            time += Time.deltaTime;

            if (time > 0.3f)
            {
                if(shotCheck == false)
                {
                    shotCheck = true;
                    audioController.vulkanShot.Play();
                }
            }
        }
        else if(inputcheck.LTriggerClicked.stateUp)
        {
            audioController.vulkanShot.Stop();
            audioController.vulkanEnd.Play();

            shotCheck = false;
            time = 0;
        }

    }
}
