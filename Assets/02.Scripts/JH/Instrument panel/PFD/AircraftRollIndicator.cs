using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftRollIndicator : MonoBehaviour
{
    public Transform fighter;

    public Transform aircraftRollIndicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aircraftRollIndicator.localRotation = Quaternion.Euler(new Vector3(0, 0, fighter.rotation.z));
            //fighter.rotation;
    }
}
