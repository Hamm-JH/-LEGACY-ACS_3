using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterIP : MonoBehaviour
{
    public AviationManager aviationManager;

    public Transform scaleArrow;

    private Vector3 idleState;
    private Vector3 upState;

    private int boostState;

    private float upTime;
    private float downTime;

    private float currRoll;

    // Start is called before the first frame update
    void Start()
    {
        idleState = new Vector3(0, 0, 57);
        upState = new Vector3(0, 0, -115);
    }

    // Update is called once per frame
    void Update()
    {
        boostState = aviationManager.booster;
        //print(boostState);
        currRoll = scaleArrow.transform.localRotation.eulerAngles.z;
        //print(currRoll);

        if(boostState == 1)
        {
            downTime = 0;

            scaleArrow.localRotation = Quaternion.Slerp(scaleArrow.transform.localRotation
                                                    , Quaternion.Euler(upState)
                                                    , upTime);

            upTime += Time.deltaTime;
        }
        else if(boostState == 0)
        {
            upTime = 0;

            scaleArrow.localRotation = Quaternion.Slerp(scaleArrow.transform.localRotation
                                                    , Quaternion.Euler(idleState)
                                                    , downTime);

            downTime += Time.deltaTime;
        }
    }
}
