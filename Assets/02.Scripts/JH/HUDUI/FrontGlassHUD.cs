using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FrontGlassHUD : MonoBehaviour
{

    public Transform fighterBody;

    public RawImage horizontalLine;
    public Transform VerticalLine;
    public Transform hor_Line;

    public float pitch;
    public float yaw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pitch = fighterBody.eulerAngles.x;
        pitch = ((360 * (int)(pitch / 260)) - pitch) / 9 * 8;

        //수직 각도 변경
        VerticalLine.localPosition = new Vector3(66, pitch * 10, 0);

        //수평 각도 변경값 받기
        yaw = fighterBody.rotation.eulerAngles.y / 360;

        //수평 각도 변경
        horizontalLine.uvRect = new Rect(yaw, 0, 1, 1);

        hor_Line.localRotation = Quaternion.Euler(new Vector3(0, 0, fighterBody.rotation.eulerAngles.z));
    }
}
