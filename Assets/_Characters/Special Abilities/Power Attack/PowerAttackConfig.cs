using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu(menuName = ("RPG/Special Abilities/Power Attack"))]

	public class PowerAttackConfig : SpecialAbilityConfig
	{	
		[Header("Power Attack Specific")]
		[SerializeField] float abilityDamage = 40f;
		// [SerializeField] float abilityCost = 100f;
		// [SerializeField] float cooldownTime = 4f;
		// [SerializeField] float abilityRange = 10f;


		public override void AddComponentTo (GameObject gameObjectToAttachTo)
		{
			var behaviorComponent = gameObjectToAttachTo.AddComponent<PowerAttackBehavior>();
			behaviorComponent.SetConfig(this);
			behavior = behaviorComponent;
		}

		public float GetAbilityDamage()
		{
			return abilityDamage;
		}

	}
}
