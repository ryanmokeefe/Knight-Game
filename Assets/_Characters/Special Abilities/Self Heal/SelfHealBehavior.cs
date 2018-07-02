using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class SelfHealBehavior : MonoBehaviour, ISpecialAbility 
	{
		SelfHealConfig config;
		Player player;
		Enemy enemy;
		AudioSource audioSource;

		public void SetConfig(SelfHealConfig newConfig)
		{
			this.config = newConfig;
		}

		void Start()
		{
			if (GetComponent<Player>())
			{
				player = GetComponent<Player>();
			}
			else if (GetComponent<Enemy>())
			{
				enemy = GetComponent<Enemy>();
			}
			audioSource = GetComponent<AudioSource>();
		}

		public void Use(AbilityUseParams useParams)
		{
			print("Heal Ability Used by: " + gameObject.name);
			ActivateHeal(useParams);
			ActivateSound();
			PlayParticleSystem();
		}

		private void ActivateHeal(AbilityUseParams useParams)
		{
			if (player)
			{
				player.Heal(config.GetHealth());
			}
			else if (enemy)
			{
				// TODO: add HEAL to enemy script
				enemy.TakeDamage(-config.GetHealth());
			}
		}

		// TODO: move audio and particle behavior to behavior superclass
		private void ActivateSound()
		{
			audioSource.clip = config.GetAudio();
			audioSource.Play();
		}

		private void PlayParticleSystem()
		{
			GameObject particlePrefab = Instantiate(config.GetParticles(), transform.position, Quaternion.identity);
			particlePrefab.transform.parent = transform;
			ParticleSystem particles = particlePrefab.GetComponent<ParticleSystem>();
			particles.Play();
			Destroy(particlePrefab, particles.main.duration);
		}

	}
}
