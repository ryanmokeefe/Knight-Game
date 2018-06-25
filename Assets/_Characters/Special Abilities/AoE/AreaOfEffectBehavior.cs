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
			float maxDistance = Mathf.Infinity;

			float damageAmount = useParams.baseDamage + config.GetAbilityDamage();
			// affect all in sphere
			RaycastHit[] targets = Physics.SphereCastAll(origin, radius, origin, maxDistance);

			print("Number of targets" + targets.Length);
			for (var thing = 0; thing < targets.Length; thing++)
			{
				print("Enemies affected: " + targets[thing].collider.gameObject.name);
			}
			
			
			int i = 0;
			while (i < (targets.Length - 1))
			{
				var target = targets[i].collider.gameObject.GetComponent<Enemy>();
				if (target)
				{
					target.TakeDamage(damageAmount);
				}
				i++;
			}

		}
	}
}
