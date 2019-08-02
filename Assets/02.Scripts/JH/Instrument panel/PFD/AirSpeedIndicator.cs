using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSpeedIndicator : MonoBehaviour
{
    public PFDManager PFD;

    public Transform speedIndicator;

    private float _currForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //속도 입력값
        _currForce = PFD.aviMeter.thrustControl.currForce / 4;
        
        //속도계 위치값 변경
        speedIndicator.localPosition = new Vector3(0, 700 - (_currForce / 2125 * 1257), 0);
    }
}
