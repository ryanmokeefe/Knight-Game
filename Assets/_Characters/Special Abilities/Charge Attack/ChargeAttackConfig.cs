using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Characters
{
	[CreateAssetMenu(menuName = ("RPG/Special Abilities/Charge Attack"))]

	public class ChargeAttackConfig : SpecialAbilityConfig 
	{
		[Header("Charge Attack Specific")]

		[SerializeField] float abilityDamage = 40f;
		[SerializeField] float extraDamage = 10f;
		[SerializeField] float abilityRadius = 5f;
		[SerializeField] float abilitySpeed = 10f;

		public override void AddComponentTo(GameObject gameObjectToAttachTo)
		{
			var behaviorComponent = gameObjectToAttachTo.AddComponent<ChargeAttackBehavior>();
			behaviorComponent.SetConfig(this);
			behavior = behaviorComponent;
		}

		public float GetDamage()
		{
			return abilityDamage;
		}

		public float GetExtraDamage()
		{
			return extraDamage;
		}

		public float GetRadius()
		{
			return abilityRadius;
		}
		
		public float GetSpeed()
		{
			return abilitySpeed;
		}
	}
}
