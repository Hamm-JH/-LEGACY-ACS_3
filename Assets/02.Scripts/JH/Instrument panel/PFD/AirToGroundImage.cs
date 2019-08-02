using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirToGroundImage : MonoBehaviour
{
    public Transform fighter;
    public Transform airToGroundImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(airToGroundImage.localRotation.eulerAngles.z);
        airToGroundImage.localRotation = Quaternion.Euler(new Vector3(0, 0, fighter.rotation.eulerAngles.z));
    }
}
