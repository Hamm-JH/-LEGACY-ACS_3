using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SetCamposition : MonoBehaviour
{
    public Transform cam;
    public Transform camPosition;
	public Transform body;

	void Start()
	{
		camPosition.position = body.position;
		cam.position = camPosition.position;
		UnityEngine.XR.InputTracking.disablePositionalTracking = true;
	}

	// Update is called once per frame
	void Update()
    {
		camPosition.position = body.position;
		cam.position = camPosition.position;
	}
    
}
