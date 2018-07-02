using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class PowerAttackBehavior : MonoBehaviour, ISpecialAbility 
	{
		PowerAttackConfig config;

		public void SetConfig(PowerAttackConfig newConfig)
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
			float damageAmount = useParams.baseDamage + config.GetAbilityDamage();
			useParams.target.TakeDamage(damageAmount);
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
