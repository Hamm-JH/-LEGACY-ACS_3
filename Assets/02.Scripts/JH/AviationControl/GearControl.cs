using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearControl : MonoBehaviour
{
    [Header ("input check")]
    public ControllerInputCheck inputCheck;

    [Header("fighter animator")]
    public Animator fighterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inputCheck.RGrabGrip.stateDown)
        {
            //GearState 변경. 한번 누를때마다 기어 올리고, 내리고 번갈아가면서 함
            if(fighterAnimator.GetBool("GearState") == true)
            {
                fighterAnimator.SetBool("GearState", false);
            }
            else if(fighterAnimator.GetBool("GearState") == false)
            {
                fighterAnimator.SetBool("GearState", true);
            }
        }
        else if(inputCheck.RGrabGrip.state)
        {
            //print("state : " + inputCheck.RGrabGrip.state);
        }
        else if(inputCheck.RGrabGrip.stateUp)
        {
            //print("stateUp : " + inputCheck.RGrabGrip.stateUp);
        }
    }
}
