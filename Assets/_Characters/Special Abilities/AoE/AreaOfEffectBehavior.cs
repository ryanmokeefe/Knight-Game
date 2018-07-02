using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class AreaOfEffectBehavior : MonoBehaviour, ISpecialAbility 
	{
		AreaOfEffectConfig config;

		public void SetConfig(AreaOfEffectConfig newConfig)
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
			print("AoE attack used by: " + gameObject.name);
			Vector3 origin = gameObject.transform.position;
			float radius = config.GetRadius();

			float damageAmount = useParams.baseDamage + config.GetAbilityDamage();
			RaycastHit[] targets = Physics.SphereCastAll(origin, radius, origin, radius);

			foreach (RaycastHit hit in targets)
			{
				var target = hit.collider.gameObject.GetComponent<IDamageable>();
				// TODO: consider using Tag or layer, so enemies don't damage each other
				if (target != null && target != useParams.user)
				{
					target.TakeDamage(damageAmount);
				}
			}
		}

		private void PlayParticleSystem()
		{
			GameObject particlePrefab = Instantiate(config.GetParticles(), transform.position, Quaternion.identity);
			ParticleSystem particleSystem = particlePrefab.GetComponent<ParticleSystem>();
			particleSystem.Play();
			Destroy(particlePrefab, particleSystem.main.duration);
		}
	}
}
