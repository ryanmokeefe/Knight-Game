using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class RangedAoEBehavior : MonoBehaviour, ISpecialAbility 
	{
		RangedAoEConfig config;

		public void SetConfig(RangedAoEConfig newConfig)
		{
			this.config = newConfig;
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void Use(AbilityUseParams useParams)
		{
			print("AoE attack used by: " + gameObject.name);
			// TODO: change origin to useParams.target for a TARGETED AoE (currently AoE encircles player)
			Vector3 origin = gameObject.transform.position;
			float radius = config.GetRadius();

			float damageAmount = useParams.baseDamage + config.GetAbilityDamage();
// change origin to target ENEMY // Change movement for BLAST attack
			RaycastHit[] targets = Physics.SphereCastAll(origin, radius, origin, radius);

			// print("Number of targets" + targets.Length);
			// for (var thing = 0; thing < targets.Length; thing++)
			// {
			// 	print("Raycasts Hit: " + targets[thing].collider.gameObject.name);
			// }
			
			foreach (RaycastHit hit in targets)
			{
				var target = hit.collider.gameObject.GetComponent<IDamageable>();
				if (target != null)
				{
					target.TakeDamage(damageAmount);
					// print("Enemy Hit: " + target);
				}
			}

		}
	}
}
