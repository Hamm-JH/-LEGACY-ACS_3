using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F16HUDManager : MonoBehaviour
{
    public enum HUDState
    {
        FlightState = 1,
        FightState = 2
    }

    [Header("HUD changer")]
    public F16HUDChanger HUDChanger;

    [Header("input check")]
    public ControllerInputCheck inputCheck;

    //[Header("input values")]

    [Header("HUD state")]
    [HideInInspector] public HUDState stateHUD;

    // Start is called before the first frame update
    void Start()
    {
        stateHUD = HUDState.FlightState;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputCheck.LGrabGrip.stateDown)
        {
            changeState();
        }
    }

    private void changeState()
    {
        switch(stateHUD)
        {
            case HUDState.FlightState:
                stateHUD = HUDState.FightState;
                print("fightMode");
                HUDChanger.changeHUD(stateHUD);
                break;

            case HUDState.FightState:
                stateHUD = HUDState.FlightState;
                print("flightMode");
                HUDChanger.changeHUD(stateHUD);
                break;
        }
    }
}
