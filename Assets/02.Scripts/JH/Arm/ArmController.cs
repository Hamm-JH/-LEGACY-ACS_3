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

    [Header("Arms")]
    public GameObject vulkanBullet;

    [Header("Arm position")]
    public Transform vulkanPosition;

    [Header("remain Arms values")]
    public int Vulkans;

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

        Vulkans = 800;
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

            if(Vulkans > 0)
            {
                if (time > 0.3f)
                {
                    if(shotCheck == false)
                    {
                        shotCheck = true;
                        audioController.vulkanShot.Play();

                    }
                    Instantiate(vulkanBullet, vulkanPosition.localPosition, vulkanPosition.localRotation);
                    --Vulkans;
                }
            }
            //무기 다 떨어질 경우
            else
            {
                audioController.vulkanShot.Stop();
                audioController.vulkanEnd.Play();

                shotCheck = false;
                time = 0;
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
