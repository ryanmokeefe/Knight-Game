using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class PowerAttackBehavior : SpecialAbilityBehavior 
	{
		public override void Use(AbilityUseParams useParams)
		{
			PlayAnimation();
			PlayParticleSystem();
			DealDamage(useParams);
		}

		private void DealDamage(AbilityUseParams useParams)
		{
			float damageAmount = useParams.baseDamage + (config as PowerAttackConfig).GetAbilityDamage();
			useParams.target.TakeDamage(damageAmount);
		}
	}	
}
