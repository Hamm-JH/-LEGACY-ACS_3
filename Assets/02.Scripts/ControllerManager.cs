using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class ControllerManager : MonoBehaviour
{
    [Header ("출력 텍스트")]
    public Text LText;
    public Text RText;

    [Header ("컨트롤러 종류")]
    public SteamVR_Input_Sources LeftController     = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources RightController    = SteamVR_Input_Sources.RightHand;

    [Header ("왼쪽 컨트롤러 변수")]
    public SteamVR_Action_Boolean interactUI;
    public SteamVR_Action_Boolean Lt_GrabPinch;
    public SteamVR_Action_Boolean Lt_GrabGrip;
    public SteamVR_Action_Boolean Lt_Trackpad;
    public SteamVR_Action_Boolean Lt_Menu;
    public SteamVR_Action_Vector2 Lt_TrackpadPos;
    public SteamVR_Action_Pose Lt_pose;

    [Header ("오른쪽 컨트롤러 변수")]
    public SteamVR_Action_Boolean Rt_GrabPinch;
    public SteamVR_Action_Boolean Rt_GrabGrip;
    public SteamVR_Action_Boolean Rt_Trackpad;
    public SteamVR_Action_Boolean Rt_Menu;
    public SteamVR_Action_Vector2 Rt_TrackpadPos;
    public SteamVR_Action_Pose Rt_pose;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("interactUI " + interactUI.state);
        LText.text = "Lt_GrabPinch  "   + Lt_GrabPinch.state
                   + "\nLt_GrabGrip   " + Lt_GrabGrip.state
                   + "\nLt_Trackpad   " + Lt_Trackpad.state
                   + "\nLt_Menu     "   + Lt_Menu.state
                   + "\nLt_TrackpadPos  " + Lt_TrackpadPos.axis
                   + "\nLt_pose.localPosition "   + Lt_pose.localPosition
                   + "\nLt_pose.localRotation "   + Lt_pose.localRotation;

        RText.text = "Rt_GrabPinch  " + Rt_GrabPinch.state
                   + "\nRt_GrabGrip   " + Rt_GrabGrip.state
                   + "\nRt_Trackpad   " + Rt_Trackpad.state
                   + "\nRt_Menu     " + Rt_Menu.state
                   + "\nRt_TrackpadPos  " + Rt_TrackpadPos.axis
                   + "\nRt_pose.localPosition " + Rt_pose.localPosition
                   + "\nRt_pose.localRotation " + Rt_pose.localRotation;

    }
}
