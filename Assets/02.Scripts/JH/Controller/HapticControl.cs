using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticControl : MonoBehaviour
{
    [Header ("input check")]
    public ControllerInputCheck inputCheck;

    public void HapticOn(float duration, float rand)
    {
        inputCheck.LHaptic.Execute(0, duration, 50 * rand, 35 * rand, inputCheck.leftController);
        inputCheck.LHaptic.Execute(0, duration, 50 * rand, 35 * rand, inputCheck.rightController);
    }
}
