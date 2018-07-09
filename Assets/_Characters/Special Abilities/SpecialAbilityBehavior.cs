using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public abstract class SpecialAbilityBehavior : MonoBehaviour, ISpecialAbility 
	{
		protected SpecialAbilityConfig config;
		const string ATTACK_TRIGGER = "Attack";
		const string DEFAULT_ATTACK_STATE = "Default Attack";

		public abstract void Use(AbilityUseParams useParams);

		public void SetConfig(SpecialAbilityConfig newConfig)
		{
			config = newConfig;
		}

		protected void PlayAnimation()
		{
			AnimationClip anim = config.GetAnim();

			var animOverride = GetComponent<Player>().GetAnimOverrideController();
			var animator = GetComponent<Animator>(); 
			animator.runtimeAnimatorController = animOverride;
			animOverride[DEFAULT_ATTACK_STATE] = anim;
			animator.SetTrigger(ATTACK_TRIGGER);

		}

		protected void PlayParticleSystem()
		{
			Vector3 newPosition = transform.position + new Vector3(0, 1, 0);
			var particlePrefab = config.GetParticles();
			var particleObject = Instantiate(
				particlePrefab, 
				newPosition, 
				particlePrefab.transform.rotation
			);
			particleObject.transform.parent = transform;
			var particleSystem = particleObject.GetComponent<ParticleSystem>();
			particleSystem.Play();
			Destroy(particleObject, particleSystem.main.duration);

		}

		// TODO: add sound to play on use


	}
}
