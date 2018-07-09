using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class ChargeAttackBehavior : SpecialAbilityBehavior 
	{

		public override void Use(AbilityUseParams useParams)
		{
			// PlayAnimation();
			// PlayParticleSystem();
			DealDamage(useParams);
		}

		private void DealDamage(AbilityUseParams useParams)
		{
			MonoBehaviour newTarget = useParams.target as MonoBehaviour;
			MonoBehaviour newUser = useParams.user as MonoBehaviour;


			Vector3 destination = newTarget.gameObject.transform.position;
			float speed = (config as ChargeAttackConfig).GetSpeed();
			float damage = (config as ChargeAttackConfig).GetDamage();
			float extraDamage = (config as ChargeAttackConfig).GetExtraDamage();
			float radius = (config as ChargeAttackConfig).GetRadius();
			var player = newUser.gameObject;
			var target = newTarget.gameObject;

			print(newTarget.transform.position);
			print(newUser.transform.position);
			
			// player.transform.position = target.transform.position;

			// print("Charge attack used by: " + gameObject.name);
			// Vector3 origin = gameObject.transform.position;
			// float radius = (config as ChargeAttackConfig).GetRadius();

			// float damageAmount = useParams.baseDamage + (config as ChargeAttackConfig).GetAbilityDamage();
			// RaycastHit[] targets = Physics.SphereCastAll(origin, radius, origin, radius);
			// 	// print("AoE Hit List: " + targets);

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
