using UnityEngine;
using System.Collections;

/// <summary>
/// Every GameObject that has this script attached gets tracked by the radar.
/// </summary>
public class TrackedObject : MonoBehaviour {

	public BlipTypes BlipType = BlipTypes.Dot;

	[Range(0.1f, 5)]
	public float BlipSize = 1;

	public bool OverrideBlipColor = false;
	public Color OverrideColor = Color.green;

	void Start () {
		TrackObject();
	}

	void OnDestroy() {
		DoNotTrackObject();
	}

	/// <summary>
	/// Registers the object to be tracked by the radar
	/// </summary>
	public void TrackObject() {
		RadarController.RegisterTrackedObject(this);
	}

	/// <summary>
	/// Unregisters the object from the radar and it will no longer be tracked
	/// </summary>
	public void DoNotTrackObject() {
		RadarController.UnregisterTrackedObject(this);
	}
	
	public enum BlipTypes {
		Dot,
		Diamond,
		X,
		Square,
		Triangle
	}

}
