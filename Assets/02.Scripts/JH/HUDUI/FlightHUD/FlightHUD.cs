using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlightHUD : MonoBehaviour
{
    public Transform fighterBody;
    public Transform cam;       //HeadingMeter 바꿀 카메라각도

    public RawImage headingMeter;   //보는 방향 알려주는 눈금

    public Transform altiMeter;     //고도계 확인용
    public Text altiMeterText;      //고도 확인 텍스트

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
    }
}
