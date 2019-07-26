using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerInputCheck : MonoBehaviour
{
	public SteamVR_Input_Sources rightController;
	public SteamVR_Action_Boolean isTriggerClicked;     //트리거 클릭 확인
	public SteamVR_Action_Vector2 isTrackpadDragged;    //트랙패드 드래깅 확인
	public SteamVR_Action_Pose poseTracker;

	[HideInInspector] public bool booster = false;
	[HideInInspector] public float throttle = 0;
	[HideInInspector] public Quaternion controllerAngle;

	//public FlameManager testMgr;

    // Update is called once per frame
    void Update()
    {
		//컨트롤러 각도 갱신
		controllerAngle = poseTracker.localRotation;
		
		//트리거 눌렀을 때(한번)
		if(isTriggerClicked.stateDown)
		{
			booster = true;             //print(booster);
			//testMgr.LFlame.material.Lerp(testMgr.BReady, testMgr.RReady, 1f);

		}
		//트리거 뗐을 때(한번)
		else if(isTriggerClicked.stateUp)
		{
			booster = false;            //print(booster);
			//testMgr.LFlame.material.Lerp(testMgr.RReady, testMgr.BReady, 1f);
		}

		//트랙패드 값 변경시
		if(isTrackpadDragged.changed)
		{
			throttle = Mathf.Lerp(throttle
								  , ((isTrackpadDragged.axis.y + 1f) / 2)
								  , 0.1f);
		}



		//print(poseTracker.angularVelocity);		//각가속도
		//print(poseTracker.changed);				//벡터변화 확인
		//print(poseTracker.velocity);				//컨트롤러 이동속도

		//float pitch = Mathf.Clamp(poseTracker.localRotation.x, Mathf.Epsilon - 0.8f, Mathf.Epsilon);
		//float yaw = Mathf.Clamp(poseTracker.localRotation.y, Mathf.Epsilon - 0.7f, Mathf.Epsilon + 0.7f);
		//float roll = Mathf.Clamp(poseTracker.localRotation.z, Mathf.Epsilon - 0.7f, Mathf.Epsilon + 0.7f);
	}
}
