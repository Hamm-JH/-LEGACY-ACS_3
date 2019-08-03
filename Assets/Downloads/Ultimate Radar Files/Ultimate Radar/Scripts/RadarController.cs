using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadarController : MonoBehaviour {
	public GameObject RadarCenter; //The object that is the center of the radar

	public float BeamSpeed = 5f; //The speed of the radar beam
	public float RadarRange = 40; //The range of the radar in units

	public Color32 DefaultBlipColor = Color.green;

	public bool FixedRadarRotation = true;

	public RadarModes RadarMode;
	public RadarTypes RadarType;

	public GameObject blipDotPrefab; //The dot blip object
	public GameObject blipDiamondPrefab; //The diamond blip object
	public GameObject blipXPrefab; //The X blip object
	public GameObject blipSquarePrefab; //The Square blip dot object
	public GameObject blipTrianglePrefab; //The Triangle blip dot object

	public bool BlipOpacityChange = true; //If true the opacity of the blips changes over time in realistic mode

	public bool ShowBeam = true;

	public GameObject Beam; //The Beam object

	[Header("3D Radar options")]
	public float LineWidth = 0.1f;
	public float HeightOffset = 0;
	public bool RelativeLineOffset = true;
	public Material LineMaterial;
	public GameObject RadarCenterBlipPrefab;
	public GameObject RadarCamera;

	private List<BlipContainer> trackedObjects;
	private List<TrackedObject> localTrackedObjects; //we add the tracked objects here so that we can have more than one radars simultaneously.

	private static List<TrackedObject> objectsToTrack; //We store the objects here first in order for them to be registered for tracking even if the radar is disabled
	private static List<TrackedObject> objectsToStopTracking; //same as objectsToTrack

	private GameObject radarCenterBlip;

	public enum RadarModes {
		Realistic,
		Realtime
	};

	public enum RadarTypes {
		Normal,
		ThreeDimensional
	};
	
	void Start () {
		localTrackedObjects = new List<TrackedObject>();
		objectsToTrack = null;
		objectsToStopTracking = null;
		if(trackedObjects == null) {
			trackedObjects = new List<BlipContainer>();
		}

		if(RadarType == RadarTypes.ThreeDimensional && RadarCenterBlipPrefab != null) {
			radarCenterBlip = GameObject.Instantiate<GameObject>(RadarCenterBlipPrefab);
			radarCenterBlip.transform.SetParent(transform);
			radarCenterBlip.transform.position = Vector3.zero;
			radarCenterBlip.transform.rotation = Quaternion.identity;
			radarCenterBlip.layer = Beam.layer;
		}

	}

	void Update() {
		checkObjectTrackingChanges();
		Vector3 centerPosition = RadarCenter.transform.position;
		centerPosition.y = 0; //We assume that the y axis is up, so we get rid of it to work on the xz plane.
		Beam.SetActive(ShowBeam);
		foreach(BlipContainer blipContainer in trackedObjects) {
			GameObject obj = blipContainer.trackedObject;

			Vector3 objectPosition = obj.transform.position;
			objectPosition.y = 0; //Same as the centerPosition

			float distanceFromCenter = Vector3.Distance(centerPosition, objectPosition);
			if(distanceFromCenter < RadarRange) {

				//We find the % of the distance of the object to the radar's range.
				//100% of this value means that the tracked object is right at the edge of the radar's range and 0% is right
				//on top of the radar center object.
				float distancePercentage = distanceFromCenter / RadarRange;

				Vector3 blimpPos = objectPosition - centerPosition;

				Vector3 blPosActual = new Vector3(blimpPos.normalized.x * 2.5f * distancePercentage, blimpPos.normalized.z * 2.5f * distancePercentage, blimpPos.normalized.z * 2.5f * distancePercentage);
				blPosActual.z += HeightOffset;
				if(RadarType != RadarTypes.ThreeDimensional) {
					blPosActual.z = 0;
				}
				float angle = Vector3.Angle(Vector3.up, blPosActual);

				//Since Vector2.Angle is between 0-180, we need to find the sign of the angle in order to convert it to 0-360
				Vector3 cross = Vector3.Cross(Vector3.up, blPosActual);

				if(cross.z > 0) {
					angle = 360 - angle;
				}
				angle = 360 - angle;

				angle = angle + 90;

				if(angle >= 360) {
					angle = angle - 360;
				}

				//If the radar doesn't have a fixed rotation, we need to apply the rotation of the radar center object
				//to the calculated angle. As before, we assume that the y axis is up.
				if(!FixedRadarRotation) { 
					angle = angle + RadarCenter.transform.rotation.eulerAngles.y;

					blPosActual = Quaternion.Euler(0, 0, RadarCenter.transform.rotation.eulerAngles.y) * blPosActual;

					if(angle > 360) {
						angle = angle - 360;
					}
				}

				if(Beam != null) {

					float beamAngle = Beam.transform.localRotation.eulerAngles.z;

					if(beamAngle < angle && blipContainer.dirty || RadarMode == RadarModes.Realtime) {
						blipContainer.blip.SetActive(true);

						blipContainer.blip.transform.localPosition = blPosActual;
						blipContainer.blip.transform.localRotation = Quaternion.identity;

						if(RadarType == RadarTypes.ThreeDimensional) {

							blipContainer.line.transform.rotation = Quaternion.identity;
							blipContainer.line.transform.position = Vector3.zero;
							blipContainer.line.SetPosition(0, blipContainer.blip.transform.position);
							Vector3 linePosEnd = blPosActual;
							if(RelativeLineOffset) {
								linePosEnd.z = HeightOffset;
							} else {
								linePosEnd.z = 0;
							}
							linePosEnd = transform.TransformPoint(linePosEnd);

							blipContainer.line.SetPosition(1, linePosEnd);
							if(RadarCamera != null) {
								blipContainer.blip.transform.LookAt(RadarCamera.transform.position);
								if(radarCenterBlip != null) {
									radarCenterBlip.transform.LookAt(RadarCamera.transform.position);
								}
							}
						}

						blipContainer.dirty = false;
						if(RadarMode == RadarModes.Realistic) {
							BlipController blipController = blipContainer.blip.GetComponent<BlipController>();
							blipController.EnableStretcher();
							blipController.SetOpacityChange(BlipOpacityChange);
						}
					}
				}

			} else {
				blipContainer.blip.SetActive(false);
			}

		}

		if(radarCenterBlip != null && RadarType == RadarTypes.ThreeDimensional) {
			radarCenterBlip.transform.localPosition = new Vector3(0, 0, HeightOffset);
		}

	}

	/// <summary>
	/// Checks the objectsToTrack and objectsToStopTracking lists for changes and updates the trackedObjects list as needed
	/// </summary>
	private void checkObjectTrackingChanges() {
		if(objectsToTrack != null && objectsToTrack.Count > 0) {
			lock(objectsToTrack) {
				//enable the tracking of the objects in the objectToTrackList
				foreach(TrackedObject trObject in objectsToTrack) {
					if(!localTrackedObjects.Contains(trObject)) {
						enableTrackedObject(trObject);
						localTrackedObjects.Add(trObject);
					}
				}
				//objectsToTrack.Clear();
			}
		}

		if(objectsToStopTracking != null && objectsToStopTracking.Count > 0) {
			lock(objectsToStopTracking) {
				//disable the tracking of the objects in the objectsToStopTrackingList
				foreach(TrackedObject trObject in objectsToStopTracking) {
					if(localTrackedObjects.Contains(trObject)) {
						disableTrackedObject(trObject);
						localTrackedObjects.Remove(trObject);
					}
				}
				//objectsToStopTracking.Clear();
			}
		}
	}

	/// <summary>
	/// Enables the tracking of an object
	/// </summary>
	/// <param name="trackedObject">The object to be tracked</param>
	private void enableTrackedObject(TrackedObject trackedObject) {
		//disable the tracked object first to avoid having duplicate blips
		disableTrackedObject(trackedObject);

		GameObject obj = trackedObject.gameObject;

		if(trackedObjects == null) {
			trackedObjects = new List<BlipContainer>();
		}
		BlipContainer blipContainer = new BlipContainer();
		blipContainer.trackedObject = obj;
		
		//apply the selected blip type
		GameObject blipObject = blipDotPrefab;
		switch(trackedObject.BlipType) {
		case TrackedObject.BlipTypes.Diamond:
			blipObject = blipDiamondPrefab;
			break;
			
		case TrackedObject.BlipTypes.Square:
			blipObject = blipSquarePrefab;
			break;
			
		case TrackedObject.BlipTypes.Triangle:
			blipObject = blipTrianglePrefab;
			break;
			
		case TrackedObject.BlipTypes.X:
			blipObject = blipXPrefab;
			break;
		}
		
		//Instantiate the blip object and added to the radar
		blipContainer.blip = (GameObject) Instantiate(blipObject);
		if(trackedObject.OverrideBlipColor) {
			blipContainer.blip.GetComponent<SpriteRenderer>().color = trackedObject.OverrideColor;
		} else {
			blipContainer.blip.GetComponent<SpriteRenderer>().color = DefaultBlipColor;
		}
		trackedObjects.Add(blipContainer);
		blipContainer.blip.transform.parent = transform;
		blipContainer.blip.transform.position = new Vector3(0, 0, 0);
		blipContainer.blip.transform.localRotation = Quaternion.identity;
		blipContainer.blip.transform.localScale = new Vector3(1, 1, 1);
		blipContainer.blip.SetActive(false);
		blipContainer.blip.layer = Beam.layer;

		if(RadarType == RadarTypes.ThreeDimensional) {
			GameObject lineObj = new GameObject();
			lineObj.transform.SetParent(blipContainer.blip.transform);
            lineObj.layer = gameObject.layer;
			LineRenderer line = lineObj.AddComponent<LineRenderer>();

			line.material = LineMaterial;
			line.SetWidth(LineWidth, LineWidth);
			line.SetPosition(0, Vector3.zero);
			line.SetPosition(1, Vector3.zero);
			line.SetColors(DefaultBlipColor, DefaultBlipColor);
			line.useWorldSpace = true;

			blipContainer.line = line;
		}

		//Pass the tracked object to the blip controller object to keep track of the blip size on runtime
		BlipController blipController = blipContainer.blip.GetComponent<BlipController>();
		blipController.setTrackedObject(trackedObject);
	}

	/// <summary>
	/// Disables the tracking of an object
	/// </summary>
	/// <param name="trackedObject">The tracked object.</param>
	private void disableTrackedObject(TrackedObject trackedObject) {
		if(trackedObject != null) {
			GameObject obj = trackedObject.gameObject;
			
			//Find the blip container of the object to be removed from the tracked objects list
			BlipContainer objectBlipContainer = null;
			foreach(BlipContainer blipContainer in trackedObjects) {
				if(blipContainer.trackedObject.Equals(obj)) {
					objectBlipContainer = blipContainer;
					break;
				}
			}
			
			//Remove the object from the tracked objects list and delete its blip game object
			if(objectBlipContainer != null) {
				trackedObjects.Remove(objectBlipContainer);
				GameObject.Destroy(objectBlipContainer.blip);
			}
		} else {
			BlipContainer objectBlipContainer = null;
			foreach(BlipContainer blipContainer in trackedObjects) {
				if(blipContainer.trackedObject == null) {
					objectBlipContainer = blipContainer;
					break;
				}
			}
			if(objectBlipContainer != null) {
				GameObject.Destroy(objectBlipContainer.blip);
				trackedObjects.Remove(objectBlipContainer);
			}
		}
	}

	public BlipContainer GetBlipContainer(GameObject blipObj) {
		BlipContainer retCont = null;
		foreach(BlipContainer blipContainer in trackedObjects) {

			if(blipContainer.blip.Equals(blipObj)) {
			
				retCont = blipContainer;
				break;
			}

		}

		return retCont;
	}

	public List<BlipContainer> GetTrackedObjects() {
		return trackedObjects;
	}

	/// <summary>
	/// Rotates the beam and notifies the blips that need to be updated whenever the beam passes from the 0 angle.
	/// </summary>
	void FixedUpdate () {
		Beam.transform.RotateAround(transform.position, transform.forward, -BeamSpeed);
		float beamAngle = Beam.transform.localRotation.eulerAngles.z;
		if(beamAngle > 360 - BeamSpeed - 1) {
			foreach(BlipContainer blp in trackedObjects) {
				blp.dirty = true;
			}
		}
		Beam.transform.localPosition = new Vector3(Beam.transform.localPosition.x, Beam.transform.localPosition.y, 0);
	}

	/// <summary>
	/// Registers an object to be tracked by the radar.
	/// </summary>
	/// <param name="obj">The object to be tracked</param>
	public static void RegisterTrackedObject(TrackedObject trackedObject) {
		if(objectsToTrack == null) {
			objectsToTrack = new List<TrackedObject>();
		}
		lock(objectsToTrack) {
			objectsToTrack.Add(trackedObject);
		}
	}

	/// <summary>
	/// Unregisters an object from the radar
	/// </summary>
	/// <param name="trackedObject">Tracked object</param>
	public static void UnregisterTrackedObject(TrackedObject trackedObject) {
		if(objectsToStopTracking == null) {
			objectsToStopTracking = new List<TrackedObject>();
		}
		lock(objectsToStopTracking) {
			objectsToStopTracking.Add(trackedObject);
		}
		if(objectsToTrack.Contains(trackedObject)) {
			objectsToTrack.Remove(trackedObject);
		}
	}

	public class BlipContainer {
		public GameObject trackedObject;
		public GameObject blip;
		public LineRenderer line;
		public bool dirty = false;
	}
}
