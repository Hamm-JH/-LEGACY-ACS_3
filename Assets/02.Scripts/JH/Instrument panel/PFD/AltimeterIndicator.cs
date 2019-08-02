using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltimeterIndicator : MonoBehaviour
{
    public PFDManager PFD;

    public Transform fighterAltitude;

    public Transform altiIndicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //전투기 현재고도
        //print(fighterAltitude.position.y);

        //altiIndicator.localPosition = new Vector3(0, 343.85f, 0);   //2000
        //altiIndicator.localPosition = new Vector3(0, 465.55f, 0);   //1000
        //altiIndicator.localPosition = new Vector3(0, 587.25f, 0);   //0

        //고도표시기 1000미터당 축 이동거리 121.7f
        altiIndicator.localPosition = new Vector3(0, 587.25f - (fighterAltitude.position.y * 121.7f / 1000), 0);
        //고도표시기 y위치
    }
}
