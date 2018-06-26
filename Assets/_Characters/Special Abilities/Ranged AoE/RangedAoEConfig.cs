using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Characters
{
	[CreateAssetMenu(menuName = ("RPG/Special Abilities/AoE"))]

	public class RangedAoEConfig : SpecialAbilityConfig 
	{
		[Header("Area Of Effect Specific")]

		[SerializeField] float abilityDamage = 40f;
		[SerializeField] float abilityRadius = 20f;

		public override void AddComponentTo(GameObject gameObjectToAttachTo)
		{
			var behaviorComponent = gameObjectToAttachTo.AddComponent<RangedAoEBehavior>();
			behaviorComponent.SetConfig(this);
			behavior = behaviorComponent;
		}

		public float GetAbilityDamage()
		{
			return abilityDamage;
		}

		public float GetRadius()
		{
			return abilityRadius;
		}

	}

}
