using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRadarIP : MonoBehaviour
{
    public Transform fighterBody;

    public Transform radar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = fighterBody.rotation.eulerAngles.y;

        radar.localRotation = Quaternion.Euler( new Vector3(0, 0,
                            360 - ((y + 180) % 360)) );
        //12시부터 시계방향으로 0 + y 한바퀴
        //print((y + 180) % 360);
        //print(y - 180 * (int)(y / 180));

        //12시부터 시계방향으로 360 - z 한바퀴
        //print(radar.localRotation.eulerAngles.z);
    }
}
