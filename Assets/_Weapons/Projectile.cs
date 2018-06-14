using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{
	public class Projectile : MonoBehaviour {

		// Speed and Dmg set by other classes
		public float projectileSpeed = 10f;
		float damageCaused;
		const float destroyDelay = 0.1f;
		[SerializeField] GameObject shooter;


		public void SetShooter(GameObject shooter) 
		{
			this.shooter = shooter;
		}


		// use void to set damage, while hiding damageCaused from inspector
		public void SetDamage(float damage) 
		{
			damageCaused = damage;
		}

		// TODO: Refactor HEALS into new, make same-layer compatible ONLY

		public void OnCollisionEnter(Collision other)
		{
			// can't use chevrons <> to specify GetComp by TYPE, must use () - Unity glitch with interfaces
			Component damageable = other.gameObject.GetComponent(typeof(IDamageable));
			if (damageable && shooter.layer != other.gameObject.layer) 
			{
				(damageable as IDamageable).TakeDamage(damageCaused);
			}
			// wait to visualize trail renderer, then destroy
				Destroy(gameObject, destroyDelay);
		}

	}
}
