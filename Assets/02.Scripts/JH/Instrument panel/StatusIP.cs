using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIP : MonoBehaviour
{
    public AviationManager aviationManager;

    public Text landingGearStatus;
    public Text afterBurnerStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
