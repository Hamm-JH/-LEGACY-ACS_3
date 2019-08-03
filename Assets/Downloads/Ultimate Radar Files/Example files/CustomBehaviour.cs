using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomBehaviour : MonoBehaviour {

    public RadarController rc;
    public GameObject visObject;

    public Transform visPosition;
    GameObject currentVis;
    public bool createNewObj;

    public Material visMaterial;

	private int objectIndex = -1;

	private RadarController.BlipContainer currentObjectBlip;

	void Update () {

        if(createNewObj)
        {
            if (visObject)
            {
                if (currentVis != null)
                {
                    Destroy(currentVis);
                }

                currentVis = Instantiate(visObject, visPosition.position, Quaternion.identity) as GameObject;

                MonoBehaviour[] allComponents = currentVis.GetComponentsInChildren<MonoBehaviour>();
                
                foreach(MonoBehaviour m in allComponents)
                {
                    m.enabled = false;
                }

                currentVis.GetComponent<Rigidbody>().isKinematic = true;

                currentVis.AddComponent<CopyRotation>();
                currentVis.GetComponent<CopyRotation>().targetTrans = visObject.transform;
                currentVis.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                Renderer[] mRenderers = currentVis.GetComponentsInChildren<Renderer>();

                foreach(Renderer r in mRenderers)
                {
                    r.material = visMaterial;
                }
            }

            createNewObj = false;
        }

	}

    public void PassNextObject()
    {
        //Pass new object from tracked objects
        createNewObj = true;
		objectIndex++;
		List<RadarController.BlipContainer> trackedObjects = rc.GetTrackedObjects();

		if(objectIndex >= trackedObjects.Count) {
			objectIndex = 0;
		}

		if(objectIndex < trackedObjects.Count) {
			visObject = trackedObjects[objectIndex].trackedObject;
			if(currentObjectBlip != null) {
				currentObjectBlip.blip.GetComponent<SpriteRenderer>().color = rc.DefaultBlipColor;
				currentObjectBlip.blip.GetComponent<SpriteRenderer>().sortingOrder = 1;
			}
			currentObjectBlip = trackedObjects[objectIndex];
			currentObjectBlip.blip.GetComponent<SpriteRenderer>().color = Color.white;
			currentObjectBlip.blip.GetComponent<SpriteRenderer>().sortingOrder = 10;
		}
    }
}
