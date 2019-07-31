using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FrontGlassHUD : MonoBehaviour
{
    public Transform camera;

    public Transform horizontalLine;
    public Transform VerticalLine;

    public float pitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(camera.localRotation.x > -0.7f && camera.localRotation.x < 0.7f)
        {
            pitch = camera.localRotation.x / 7 * 10;
        }
        print(pitch * 900);
        VerticalLine.localPosition = new Vector3(0, pitch * 900, 0);
    }
}
