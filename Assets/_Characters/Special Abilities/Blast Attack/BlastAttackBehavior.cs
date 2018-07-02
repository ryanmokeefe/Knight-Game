using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class BlastAttackBehavior : MonoBehaviour, ISpecialAbility {

		BlastAttackConfig config;

		public void SetConfig(BlastAttackConfig newConfig)
		{
			this.config = newConfig;
		}


		public void Use(AbilityUseParams useParams)
		{
			DealDamage(useParams);
			PlayParticleSystem();
		}

		private void DealDamage(AbilityUseParams useParams)
		{
			print("BLAST attack used by: " + gameObject.name);

			float damageAmount = useParams.baseDamage + config.GetDamage();
			
			float radius = config.GetRadius();
			float travelDistance = config.GetTravel();
			// TODO: get position of target - send to SphereCastAll
			// Vector3 direction = useParams.target.gameObject.transform.position;
			Vector3 origin = gameObject.transform.position;
			// RaycastHit[] targets = Physics.SphereCastAll(origin, radius, direction, travelDistance);
			// foreach (RaycastHit hit in targets)
			// {
			// 	var target = hit.collider.gameObject.GetComponent<IDamageable>();
			// 	// TODO: consider using Tag or layer, so enemies don't damage each other
			// 	if (target != null && target != useParams.user)
			// 	{
			// 		target.TakeDamage(damageAmount);
			// 	}
			// }
		}

		private void PlayParticleSystem()
		{
			// TODO: particles not spawning at user?
			GameObject particlePrefab = Instantiate(config.GetParticles(), transform.position, Quaternion.identity);			
			// ?  particlePrefab.transform.parent = transform;
			ParticleSystem particleSystem = particlePrefab.GetComponent<ParticleSystem>();
			particleSystem.Play();
			Destroy(particlePrefab, particleSystem.main.duration);
			Debug.Log("BLAST Attack particles triggered at: " + transform.position);
		}

	}
}
