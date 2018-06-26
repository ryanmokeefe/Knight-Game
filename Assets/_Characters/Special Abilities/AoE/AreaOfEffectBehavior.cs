﻿using System.Collections;
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
			Vector3 origin = gameObject.transform.position;
			float radius = config.GetRadius();

			float damageAmount = useParams.baseDamage + config.GetAbilityDamage();
			RaycastHit[] targets = Physics.SphereCastAll(origin, radius, origin, radius);

			foreach (RaycastHit hit in targets)
			{
				var target = hit.collider.gameObject.GetComponent<IDamageable>();
				if (target != null)
				{
					target.TakeDamage(damageAmount);
				}
			}

		}
	}
}
