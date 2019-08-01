using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleIP : MonoBehaviour
{
    public AviationManager aviationManager;

    public Transform scaleArrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scaleArrow.localRotation = Quaternion.Euler( new Vector3(0, 0, 180 - aviationManager.throttle * 295));
    }
}
