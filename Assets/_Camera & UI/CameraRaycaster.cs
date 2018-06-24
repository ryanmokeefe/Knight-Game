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

		// Cursor affordances:
		[SerializeField] Texture2D walkCursor = null;
		[SerializeField] Texture2D enemyCursor = null;
		[SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

		[SerializeField] const int Walkable_Layer = 8;
		float maxRaycastDepth = 100f; // Hard coded value
		// Delegates and Observer Sets:
		public delegate void MouseOverTerrain(Vector3 destination);
		public event MouseOverTerrain mouseOverTerrain;

		public delegate void MouseOverEnemy(Enemy enemy);
		public event MouseOverEnemy mouseOverEnemy;

		public void UIClicked()
		{
			print("UI clicked");
		}


		void Update()
		{
			// Check if pointer is over an interactable UI element
			if (EventSystem.current.IsPointerOverGameObject ())
			{
				// TODO: UI interaction goes here
				return; 
			}
			else
			{
				PerformRaycast();
			}
		}

		private void PerformRaycast()
		{
			// Raycast to max depth, every frame as things can move under mouse
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(RaycastForEnemy(ray)) { return; }
			if(RaycastForTerrain(ray)) { return; }

		}

		private bool RaycastForEnemy(Ray ray)
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

		private bool RaycastForTerrain(Ray ray)
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

	}
}
