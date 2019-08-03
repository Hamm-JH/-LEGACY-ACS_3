using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RadarController))]
public class RadarControllerInspector : Editor {

	public override void OnInspectorGUI() {

		DrawDefaultInspector();

	}
}
