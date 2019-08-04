using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftPitchIndicator : MonoBehaviour
{
    public AviationManager aviationManager;

    public Transform pitchIndicator;

    private float pitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aviationManager._controllerAngle.x >= -0.8f && aviationManager._controllerAngle.x <= 0)
        {
            pitch = (aviationManager._controllerAngle.x + 0.4f) / 4 * 10;
        }

        //비행체 회전각도 보여주기
        pitchIndicator.localPosition = new Vector3(0, -11.5f + pitch * 89.8f * 3, 0);
    }
}
