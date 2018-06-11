using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

// Speed and Dmg set by other classes
	public float projectileSpeed = 10f;
	float damageCaused;

	// use void to set damage, while hiding damageCaused from inspector
	public void SetDamage(float damage) 
	{
		damageCaused = damage;
	}


	public void OnTriggerEnter(Collider other)
	{
		// print("collider hit: " + other.gameObject);

		// can't use chevrons <> to specify GetComp by TYPE, must use ()
		Component damageable = other.gameObject.GetComponent(typeof(IDamageable));
		if (damageable) 
		{
			(damageable as IDamageable).TakeDamage(damageCaused);
		}
	}

}
