using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

    public Transform targetTrans;

	void Update () 
    {
        transform.rotation = targetTrans.rotation;
	
	}
}
