using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu(menuName = ("RPG/Special Abilities/Blast Attack"))]

	public class BlastAttackConfig : SpecialAbilityConfig 
	{
		[Header("Blast Attack Specific")]

		[SerializeField] float abilityDamage = 40f;
		[SerializeField] float abilityTravel = 10f;
		[SerializeField] float abilityRadius = 0.5f;

		public override void AddComponentTo (GameObject gameObjectToAttachTo)
		{
			var behaviorComponent = gameObjectToAttachTo.AddComponent<BlastAttackBehavior>();
			behaviorComponent.SetConfig(this);
			behavior = behaviorComponent;
		}

		public float GetDamage()
		{
			return abilityDamage;
		}

		public float GetTravel()
		{
			return abilityTravel;
		}

		public float GetRadius()
		{
			return abilityRadius;
		}

	}
}
