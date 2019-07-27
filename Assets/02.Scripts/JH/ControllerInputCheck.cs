using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerInputCheck : MonoBehaviour
{
	public SteamVR_Input_Sources rightController;

	public SteamVR_Action_Boolean RTriggerClicked;     //트리거 클릭 확인
	public SteamVR_Action_Vector2 RTrackpadDragged;    //트랙패드 드래깅 확인
	public SteamVR_Action_Pose RPoseTracker;

	[HideInInspector] public bool booster = false;
	[HideInInspector] public float throttle = 0;
	[HideInInspector] public Quaternion controllerAngle;

	//public FlameManager testMgr;

    // Update is called once per frame
    void Update()
    {
		//컨트롤러 각도 갱신
		controllerAngle = RPoseTracker.localRotation;
		
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

		//트랙패드 값 변경시
		if(RTrackpadDragged.changed)
		{
			throttle = Mathf.Lerp(throttle
								  , ((RTrackpadDragged.axis.y + 1f) / 2)
								  , 0.1f);
		}
	}
}
