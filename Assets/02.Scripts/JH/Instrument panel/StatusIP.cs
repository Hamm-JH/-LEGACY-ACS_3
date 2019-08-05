using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIP : MonoBehaviour
{
    [Header ("Aviation manager")]
    public AviationManager aviationManager;

    [Header("gear status")]
    public Animator gearAnimator;

    [Header ("status text")]
    public Text landingGearStatus;
    public Text afterBurnerStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gearAnimator.GetBool("GearState") == true)
        {
            landingGearStatus.text = "UP";
        }
        else if(gearAnimator.GetBool("gearState") == false) {
            landingGearStatus.text = "DOWN";
        }

        //부스터 상태 출력
        switch(aviationManager.booster)
        {
            case 0:     //부스터 안씀
                afterBurnerStatus.text = "OFF";
                break;

            case 1:     //부스터 씀
                afterBurnerStatus.text = "ON";
                break;
        }
    }
}
