using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.CameraUI 
{
	public class CursorAffordance : MonoBehaviour {

		// expose 2d texture fields for cursors
		[SerializeField] Texture2D walkCursor = null;
		[SerializeField] Texture2D enemyCursor = null;
		[SerializeField] Texture2D unknownCursor = null;
		[SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);
		// TODO: Solve issue between serialized and const conflicting
		[SerializeField] const int walkableLayer = 8;
		[SerializeField] const int enemyLayer = 9;


		// require cameraraycast.cs 
		CameraRaycaster cameraRaycaster;

		// Use this for initialization
		void Start () {

			cameraRaycaster = GetComponent<CameraRaycaster>();
		/// .layerChangeObservers = .onLayerChange
			cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged; // registering onLayerChanged method
		// TODO - consider de-registering OnLayerChanged when leaving game scenes (to menu, etc.)

		}
		

		void OnLayerChanged (int newLayer) {
		
			switch (newLayer)
			{
				case walkableLayer: 
					Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
					break;
				case enemyLayer: 
					Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
					break;
				default: 
					Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
					// Debug.LogError("Cursor to display unknown.");
				return;
			}
		}



		
	}
}
