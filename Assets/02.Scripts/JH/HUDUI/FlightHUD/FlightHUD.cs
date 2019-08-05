using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlightHUD : MonoBehaviour
{
    [Header("flight status import")]
    public AviationManager aviationManager;
    public Transform fighterBody;
    public Transform cam;       //HeadingMeter 바꿀 카메라각도

    public Animator fighterAnimator;

    [Header ("heading meter")]
    public RawImage headingMeter;   //보는 방향 알려주는 눈금

    [Header ("altitude meter")]
    public Transform altiMeter;     //고도계 확인용
    public Text altiMeterText;      //고도 확인 텍스트

    [Header("flight status")]
    public Text speedText;          //속도계
    public Text throttleText;       //출력계
    public Text gearText;           //기어 상태계

    public Transform HUDInnerRing;  //비행 각도 출력
    public Transform extendWing;    //비행각도계

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //headingMeter 출력변환
        headingMeter.uvRect = new Rect(cam.localRotation.eulerAngles.y / 360, 0, 1, 1);

        altiMeter.localPosition = new Vector3(-52.95f, -1.5f - 91.42f * fighterBody.position.y / 500, 0);

        altiMeterText.text = fighterBody.position.y.ToString("N1");

        speedText.text = aviationManager.thrustControl.currForce.ToString("N1");
        throttleText.text = (aviationManager.throttle * 40).ToString("N2");

        if(fighterAnimator.GetBool("GearState") == true)
        {
            gearText.text = "GEAR ON";
        }
        else if(fighterAnimator.GetBool("GearState") == false)
        {
            gearText.text = "GEAR OFF";
        }

        //비행체 회전각도 보여주기
        extendWing.localRotation = Quaternion.Euler(new Vector3(0, 0, 360 - fighterBody.rotation.eulerAngles.z));

        //비행체 진행방향 보여주기
        HUDInnerRing.localPosition = new Vector3(-aviationManager.roll * 50
                                                , -aviationManager.pitch * 50
                                                , 0);
    }
}
