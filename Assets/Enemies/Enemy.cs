using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

// TODO: fix enemy starting at mid health

	[SerializeField]
	float maxHealthPoints = 100f;
	float currentHealthPoints = 100f;
	public bool isAlive = true;

	// Use this for initialization
	void Start () {
		
	}
	
///

	// public void SetTarget(GameObject target) 
	// {
	// 	currentTarget = target;
	// }

	// public void ClearTarget()
	// {
	// 	currentTarget = null;
	// }

	// public bool isAttacking
	// {
	// 	get 
	// 	{
	// 		return currentTarget != null;
	// 	}
	// }

	public float healthAsPercentage 
	{
		get 
		{
			return currentHealthPoints / maxHealthPoints;
		}
	}


		public void takeDamage(int damagePoints) 
	{
		var newHealthPoints = currentHealthPoints - damagePoints;
		currentHealthPoints = Mathf.Clamp(newHealthPoints, 0, maxHealthPoints);
		// TODO: insert audio clips for being attacked:
		// AudioSource.PlayClipAtPoint(PickRandomAudioClip( AUDIO GOES HERE ), transform.position);
	}

///


	// Update is called once per frame
	void Update () {
		if (!isAlive) { return; }

		if (currentHealthPoints == 0)
		{
			isAlive = false;
			// TODO: play death noise; make ghost... 
		}

	}
}
