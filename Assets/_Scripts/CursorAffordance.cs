using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

	// expose 2d texture fields for cursors
	[SerializeField]
	Texture2D walkCursor = null;

	[SerializeField]
	Texture2D enemyCursor = null;

	[SerializeField]
	Texture2D unknownCursor = null;

	[SerializeField]
	Vector2 cursorHotspot = new Vector2(96, 96);

	// require cameraraycast.cs 
	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {

		cameraRaycaster = GetComponent<CameraRaycaster>();
/// .layerChangeObservers = .onLayerChange
		cameraRaycaster.layerChangeObservers += OnLayerChanged; // registering onLayerChanged method
// TODO - consider de-registering OnLayerChanged when leaving game scenes (to menu, etc.)

	}
	

// new

	void OnLayerChanged (Layer newLayer) {
		// print("Cursor over new layer."); // only being called on layerChange, not every update
		/// layerHit = currentLayerHit
		// switch (cameraRaycaster.layerHit)
		switch (newLayer)
		{
			case Layer.Walkable: 
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.Enemy: 
				Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.RaycastEndStop:
				Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
				break;
			default: 
				Debug.LogError("Cursor to display unknown.");
			return;
		}
	}



	
}
