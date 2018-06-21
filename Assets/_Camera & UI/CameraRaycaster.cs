using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using RPG.Characters;

namespace RPG.CameraUI 
{
	public class CameraRaycaster : MonoBehaviour
	{
		// INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
		[SerializeField] int[] layerPriorities;
		// Cursor affordances:
		[SerializeField] Texture2D walkCursor = null;
		[SerializeField] Texture2D enemyCursor = null;
		[SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

		[SerializeField] const int Walkable_Layer = 8;
		float maxRaycastDepth = 100f; // Hard coded value
		int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

		public delegate void MouseOverTerrain(Vector3 destination);
		public event MouseOverTerrain mouseOverTerrain;

		public delegate void MouseOverEnemy(Enemy enemy);
		public event MouseOverEnemy mouseOverEnemy;

		public void UIClicked()
		{
			print("UI clicked");
		}

		// // // PLAYER SCRIPT STILL USES:

		// Setup delegates for broadcasting layer changes to other classes
		// public delegate void OnCursorLayerChange(int newLayer); // declare new delegate type
		// public event OnCursorLayerChange notifyLayerChangeObservers; // instantiate an observer set

		// public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit); // declare new delegate type
		// public event OnClickPriorityLayer notifyMouseClickObservers; // instantiate an observer set
		// // TODO? declare new delegate type for RIGHT click IF notifyRight interferes with OnClickPriorityLayer
		// public event OnClickPriorityLayer notifyRightMouseClickObservers; // instantiate an observer set


		void Update()
		{
			// Check if pointer is over an interactable UI element
			if (EventSystem.current.IsPointerOverGameObject ())
			{
				// NotifyObserersIfLayerChanged (5);
				return; // Stop looking for other objects
			}
			else
			{
				PerformRaycast();
			}
		}

		void PerformRaycast()
		{
			// Raycast to max depth, every frame as things can move under mouse
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(RaycastForEnemy(ray)) { return; }
			if(RaycastForTerrain(ray)) { return; }

		}

		bool RaycastForEnemy(Ray ray)
		{
			RaycastHit hitInfo;
			Physics.Raycast(ray, out hitInfo, maxRaycastDepth);
			var gameObjectHit = hitInfo.collider.gameObject; 
			var enemyHit = gameObjectHit.GetComponent<Enemy>();
			if (enemyHit)
			{
				Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
				mouseOverEnemy(enemyHit);
				return true;
			}
			return false;
		}

		bool RaycastForTerrain(Ray ray)
		{
			RaycastHit hitInfo;
			var terrainLayerMask = 1 << Walkable_Layer;
			var terrainHit = Physics.Raycast(ray, out hitInfo, maxRaycastDepth, terrainLayerMask);
			if (terrainHit)
			{
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				mouseOverTerrain(hitInfo.point);
				return true;
			}
			return false;
		}




		/////////

			// RaycastHit[] raycastHits = Physics.RaycastAll (ray, maxRaycastDepth);

			// RaycastHit? priorityHit = FindTopPriorityHit(raycastHits);
			// if (!priorityHit.HasValue) // if hit no priority object
			// {
			// 	NotifyObserersIfLayerChanged (0); // broadcast default layer
			// 	return;
			// }

			// // Notify delegates of layer change
			// var layerHit = priorityHit.Value.collider.gameObject.layer;
			// NotifyObserersIfLayerChanged(layerHit);
			
			// // Notify delegates of highest priority game object under mouse when clicked
			// if (Input.GetMouseButton (0))
			// {
			// 	notifyMouseClickObservers (priorityHit.Value, layerHit);
			// }
			// if (Input.GetMouseButtonDown(1))
			// {
			// 	notifyRightMouseClickObservers (priorityHit.Value, layerHit);
			// }


//  // // //

		// void NotifyObserersIfLayerChanged(int newLayer)
		// {
		// 	if (newLayer != topPriorityLayerLastFrame)
		// 	{
		// 		topPriorityLayerLastFrame = newLayer;
		// 		notifyLayerChangeObservers (newLayer);
		// 	}
		// }

		// RaycastHit? FindTopPriorityHit (RaycastHit[] raycastHits)
		// {
		// 	// Form list of layer numbers hit
		// 	List<int> layersOfHitColliders = new List<int> ();
		// 	foreach (RaycastHit hit in raycastHits)
		// 	{
		// 		layersOfHitColliders.Add (hit.collider.gameObject.layer);
		// 	}

		// 	// Step through layers in order of priority looking for a gameobject with that layer
		// 	foreach (int layer in layerPriorities)
		// 	{
		// 		foreach (RaycastHit hit in raycastHits)
		// 		{
		// 			if (hit.collider.gameObject.layer == layer)
		// 			{
		// 				return hit; // stop looking
		// 			}
		// 		}
		// 	}
		// 	return null; 
		// }
	}
}
