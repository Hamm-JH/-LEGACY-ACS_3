using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F16HUDChanger : MonoBehaviour
{
    [Header("HUD manager")]
    public F16HUDManager HUDManager;

    [Header("HUD state")]
    [HideInInspector] public F16HUDManager.HUDState _stateHUD;

    [Header("Flight state panels")]
    public GameObject FlightHUDPanel;
    public GameObject FlightHUDBackPanel;

    [Header("Fight state panels")]
    public GameObject FightHUDPanel;

    // Start is called before the first frame update
    void Start()
    {
        _stateHUD = F16HUDManager.HUDState.FlightState;

        FlightHUDPanel.SetActive(true);
        FlightHUDBackPanel.SetActive(true);

        FightHUDPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeHUD(F16HUDManager.HUDState state)
    {
        _stateHUD = state;

        switch(_stateHUD)
        {
            case F16HUDManager.HUDState.FlightState:
                FlightHUDPanel.SetActive(true);

                FightHUDPanel.SetActive(false);

                FlightHUDBackPanel.SetActive(true);
                break;

            case F16HUDManager.HUDState.FightState:
                FlightHUDPanel.SetActive(false);

                FightHUDPanel.SetActive(true);

                FlightHUDBackPanel.SetActive(true);
                break;
        }
    }
}
