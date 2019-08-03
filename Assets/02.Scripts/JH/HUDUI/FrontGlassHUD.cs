using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FrontGlassHUD : MonoBehaviour
{

    public Transform fighterBody;

    public RawImage horizontalLine;
    public Transform VerticalLine;

    public float pitch;
    public float yaw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //수직 각도 변경값 받기
        if(fighterBody.rotation.x > -0.7f && fighterBody.rotation.x < 0.7f)
        {
            pitch = fighterBody.rotation.x / 7 * 10;
        }
        //print(pitch * 800);

        //수직 각도 변경
        VerticalLine.localPosition = new Vector3(0, pitch * 800, 0);

        //print(fighterBody.rotation.eulerAngles.y);

        //수평 각도 변경값 받기
        yaw = fighterBody.rotation.eulerAngles.y / 360;

        //수평 각도 변경
        horizontalLine.uvRect = new Rect(yaw, 0, 1, 1);
        //print(yaw);
    }
}
