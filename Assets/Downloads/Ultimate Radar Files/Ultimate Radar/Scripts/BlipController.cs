using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the visual behaviour of the blips
/// </summary>
public class BlipController : MonoBehaviour {

	private TrackedObject trackedObject;
	private bool stretcherEnabled = false;
	private float stretchTimer = 0;

	private bool enableOpacityChange = false;
	private float opacityTimer = 0;

	private Vector3 blipScale;

	// Update is called once per frame
	void Update () {
		if(stretcherEnabled) {
			if(stretchTimer > 0.1f) {
				blipScale = new Vector3(trackedObject.BlipSize, trackedObject.BlipSize, trackedObject.BlipSize);
				transform.localScale = blipScale;

				stretchTimer = 0;
				stretcherEnabled = false;
			} else {

				stretchTimer += Time.deltaTime;
			}

		}

		if(enableOpacityChange) {
			if(opacityTimer > 0.1f) {
				ChangeAlpha(GetComponent<Renderer>().material, GetComponent<Renderer>().material.color.a - 0.05f);

				opacityTimer = 0;
			} else {
				opacityTimer += Time.deltaTime;
			}
		}

	}

	public void setTrackedObject(TrackedObject obj) {
		trackedObject = obj;
	}

	public void EnableStretcher() {
		transform.localScale = new Vector3(2, 1, 1);
		ChangeAlpha(GetComponent<Renderer>().material, 1);
		stretcherEnabled = true;
	}

	public void SetOpacityChange(bool opacityChange) {
		enableOpacityChange = opacityChange;
	}

	private void ChangeAlpha(Material mat, float alpha)
	{
		Color oldColor = mat.color;
		Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, alpha);          
		mat.SetColor("_Color", newColor);               
	}
}
