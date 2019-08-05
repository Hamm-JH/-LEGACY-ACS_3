using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightHUD : MonoBehaviour
{
    [Header("fight status import")]
    public ArmController armController;

    [Header("flight status import")]
    public AviationManager aviationManager;
    public Transform fighterBody;
    public Transform cam;

    [Header("heading meter")]
    public RawImage headingMeter;   //보는 방향 알려주는 눈금

    [Header("flight status")]
    public Text speedText;
    public Text throttleText;
    public Transform extendWing;

    [Header("fight status")]
    public Text vulkanStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        headingMeter.uvRect = new Rect(cam.localRotation.eulerAngles.y / 360, 0, 1, 1);

        speedText.text = aviationManager.thrustControl.currForce.ToString("N1");
        throttleText.text = (aviationManager.throttle * 40).ToString("N2");

        //비행체 회전각도 보여주기
        extendWing.localRotation = Quaternion.Euler(new Vector3(0, 0, 360 - fighterBody.rotation.eulerAngles.z));

        vulkanStat.text = armController.Vulkans + " ";
    }
}
