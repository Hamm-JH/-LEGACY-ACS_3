using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviationManager : MonoBehaviour
{
	#region aviation_status_code
	public enum AviationStatus
	{
		BELOW_MINIMUM_SPEED,
		NORMAL_SPEED,
		AFTER_BURNER
	}
	#endregion

	#region public_values
	[Header ("inputCheck")]
	public ControllerInputCheck inputCheck;
	[HideInInspector] public bool booster;			//부스터 발사 확인(트리거)
	[HideInInspector] public float throttle;		//쓰로틀 발생 확인(트랙패드)
	[HideInInspector] public Quaternion _controllerAngle;   //회전각도 처리용(컨트롤러 각도)

	[HideInInspector] public Quaternion controlAngle;
	[HideInInspector] public float pitch = 0;
	[HideInInspector] public float yaw = 0;
	[HideInInspector] public float roll = 0;
	[HideInInspector] public float axis = 0;

	[Header ("ThrustControl")]
	public ThrustControl thrustControl;

	[Header ("TorqueControl")]
	public TorqueControl torqueControl;

	[Header ("Aviation_status")]
	[HideInInspector] public AviationStatus status = AviationStatus.BELOW_MINIMUM_SPEED;

	[Header ("Speed_boundary")]
	[HideInInspector] public float minimumSpeedBoundary;
	[HideInInspector] public float normalSpeedBoundary;
	[HideInInspector] public float maxSpeedBoundary;

    //애프터버너 이펙트 조정
    //[Header("Engine_particle_system")]
    //public ParticleSystem LEnParticle;
    //public ParticleSystem REnParticle;

	#endregion

	#region Awake
	private void Awake()
	{
		minimumSpeedBoundary = 1200;
		normalSpeedBoundary = 6000;
		maxSpeedBoundary = 8500;
	}
	#endregion

	// Start is called before the first frame update
	void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
		//정기 상태 갱신코드
		_controllerAngle = inputCheck.controllerAngle;	//컨트롤러 각도 갱신
		
		//회전각도 정리
		pitch	= Mathf.Clamp(_controllerAngle.x, Mathf.Epsilon - 0.8f, Mathf.Epsilon);
		yaw		= Mathf.Clamp(_controllerAngle.y, Mathf.Epsilon - 0.7f, Mathf.Epsilon + 0.7f);
		roll	= Mathf.Clamp(_controllerAngle.z, Mathf.Epsilon - 0.7f, Mathf.Epsilon + 0.7f);
		axis	= _controllerAngle.w;
		controlAngle = new Quaternion(pitch, yaw, roll, axis);

		booster = inputCheck.booster;		//부스터 입력갱신
		throttle = inputCheck.throttle;     //트랙패드 입력갱신

		thrustControl.GetStatus(status);    //비행 상태 갱신(추력제어 쪽의 상태 갱신)
		torqueControl.GetStatus(status);    //비행 상태 갱신(회전제어 쪽의 상태 갱신)


	}

	//비행상태 변경 요청받는 메서드
	public void SetStatus(AviationStatus stat)
	{
		status = stat;
	}
}
