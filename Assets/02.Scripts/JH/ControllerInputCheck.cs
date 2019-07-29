using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerInputCheck : MonoBehaviour
{
    [Header ("Input_sources")]
    public SteamVR_Input_Sources leftController;
	public SteamVR_Input_Sources rightController;

    [Header("LeftHand_controller")]
    public SteamVR_Action_Vibration LHaptic;            //진동값 받음


    [Header("RightHand_controller")]
    public SteamVR_Action_Boolean RMenuClicked;         //메뉴 버튼 클릭 확인
	public SteamVR_Action_Boolean RTriggerClicked;      //트리거 클릭 확인
	public SteamVR_Action_Vector2 RTrackpadDragged;     //트랙패드 드래깅 확인
	public SteamVR_Action_Pose RPoseTracker;            //위치값 받음
    public SteamVR_Action_Vibration RHaptic;            //진동값 받음

    [Header ("Recalculated_values")]
	[HideInInspector] public bool booster = false;
	[HideInInspector] public float throttle = 0;
	[HideInInspector] public Quaternion RcontrollerAngle;
    [HideInInspector] public bool menu = false;

    // Update is called once per frame
    void Update()
    {
		//컨트롤러 각도 갱신
		RcontrollerAngle = RPoseTracker.localRotation;
		
		//트리거 눌렀을 때(한번)
		if(RTriggerClicked.stateDown)
		{
			booster = true;

		}
		//트리거 뗐을 때(한번)
		else if(RTriggerClicked.stateUp)
		{
			booster = false;
		}

        //오른쪽 메뉴 눌렀을 때
        if(RMenuClicked.stateDown)
        {
            menu = true;
        }
        //오른쪽 메뉴 뗐을 때
        else if(RMenuClicked.stateUp)
        {
            menu = false;
        }

		//트랙패드 값 변경시
		if(RTrackpadDragged.changed)
		{
			throttle = Mathf.Lerp(throttle
								  , ((RTrackpadDragged.axis.y + 1f) / 2)
								  , 0.1f);
		}
	}
}
