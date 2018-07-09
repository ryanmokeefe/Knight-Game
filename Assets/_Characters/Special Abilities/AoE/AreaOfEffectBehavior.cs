using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class AreaOfEffectBehavior : SpecialAbilityBehavior 
	{
		public override void Use(AbilityUseParams useParams)
		{
			PlayAnimation();
			PlayParticleSystem();
			DealDamage(useParams);
		}

		private void DealDamage(AbilityUseParams useParams)
		{
			print("AoE attack used by: " + gameObject.name);
			Vector3 origin = gameObject.transform.position;
			float radius = (config as AreaOfEffectConfig).GetRadius();

			float damageAmount = useParams.baseDamage + (config as AreaOfEffectConfig).GetAbilityDamage();
			RaycastHit[] targets = Physics.SphereCastAll(origin, radius, origin, radius);
				// print("AoE Hit List: " + targets);

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

	}
}
