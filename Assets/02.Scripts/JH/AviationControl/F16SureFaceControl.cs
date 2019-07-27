using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F16SureFaceControl : MonoBehaviour
{
    [Header("Aviation status")]
    public AviationManager aviationManager;
    public float pitch;
    public float yaw;
    public float roll;

    [Header("turning_object")]
    public Transform VoletL;
    public Transform VoletR;
    public Transform AileronL;
    public Transform AileronR;
    public Transform ElevatorL;
    public Transform ElevatorR;
    public Transform Rudder;
    public Transform BrakeLT;
    public Transform BrakeLB;
    public Transform BrakeRT;
    public Transform BrakeRB;

    //회전각도 범위
    private float VoletRange;
    private float AileronRange;
    private float ElevatorRange;
    private float RudderRange;
    private float BrakeRange;

    //사용 수학함수
    public float Epsilon;
    public float PitchRollCheck;

    // Start is called before the first frame update
    void Start()
    {
        Epsilon = Mathf.Epsilon + 1 - 1;    //근사값 0으로 제대로 맞춰주기 위해 1 더하고 1 뺌
        PitchRollCheck = 0;                 //pitch값과 roll값을 확인해서 보간줄 조건확인위한 변수

        VoletRange = Epsilon + 10;
        AileronRange = Epsilon + 25;
        ElevatorRange = Epsilon + 20;
        RudderRange = Epsilon + 25;
        BrakeRange = Epsilon + 35;
    }

    // Update is called once per frame
    void Update()
    {
        pitch = -aviationManager.pitch;      //pitch값 가져옴    -1 ~ 1
        yaw = aviationManager.yaw;          //yaw값 가져옴      -1 ~ 1
        roll = aviationManager.roll;        //roll값 가져옴     -1 ~ 1
        
        VoletL.transform.localRotation = Quaternion.Euler(new Vector3(pitch * VoletRange, -34.747f, -1.253f));
        VoletR.transform.localRotation = Quaternion.Euler(new Vector3(pitch * VoletRange, 34.747f, 1.253f));

        AileronL.transform.localRotation = Quaternion.Euler(new Vector3((pitch * 2 + roll * 1.2f)/2 * AileronRange, -8.91f, -1.395f));
        AileronR.transform.localRotation = Quaternion.Euler(new Vector3((pitch * 2 + -roll * 1.2f)/2 * AileronRange, 8.91f, 1.395f));

        ElevatorL.transform.localRotation = Quaternion.Euler(new Vector3(-(pitch * 2 + roll * 1.2f)/2 * ElevatorRange, 0, 0));
        ElevatorR.transform.localRotation = Quaternion.Euler(new Vector3(-(pitch * 2 + -roll * 1.2f)/2 * ElevatorRange, 0, 0));

        Rudder.transform.localRotation = Quaternion.Euler(new Vector3(0, yaw * RudderRange, 0));
        print(pitch * 2 + roll * 1.2f);
    }
}
