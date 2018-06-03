using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

	// require cameraraycast.cs 
	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
		// get component
		cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {
		
		// return layerHit from cameraRaycaster
			print(cameraRaycaster.layerHit);

	}
}
