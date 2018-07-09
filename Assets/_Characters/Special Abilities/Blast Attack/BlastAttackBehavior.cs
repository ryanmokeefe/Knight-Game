using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class BlastAttackBehavior : SpecialAbilityBehavior 
	{
		public override void Use(AbilityUseParams useParams)
		{
			PlayAnimation();
			PlayParticleSystem();
			DealDamage(useParams);
		}

		private void DealDamage(AbilityUseParams useParams)
		{
			print("BLAST attack used by: " + gameObject.name);

			float damageAmount = useParams.baseDamage + (config as BlastAttackConfig).GetDamage();
			
			float radius = (config as BlastAttackConfig).GetRadius();
			float travelDistance = (config as BlastAttackConfig).GetTravel();
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

	}
}
