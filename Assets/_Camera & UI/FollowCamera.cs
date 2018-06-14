using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI 

{
	public class FollowCamera : MonoBehaviour {

		[SerializeField]
		private Transform player;
		[SerializeField]
		private Vector3 offset;
		[SerializeField]
		private Transform target;

		private float cameraFollowSpeed = 2f;

		// Update is called once per frame
		void Update () {

			Vector3 newPosition = player.position + offset;

			transform.position = Vector3.Lerp (transform.position, newPosition, cameraFollowSpeed * Time.deltaTime);

			transform.LookAt(target);
			
		}
	}
}
