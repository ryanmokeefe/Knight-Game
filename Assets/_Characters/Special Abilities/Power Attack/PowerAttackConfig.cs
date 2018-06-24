using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu(menuName = ("RPG/Special Abilities/Power Attack"))]

	public class PowerAttackConfig : SpecialAbilityConfig
	{	
		[Header("Power Attack Specific")]
		[SerializeField] float damage = 40f;
		// [SerializeField] float cooldownTime = 4f;
		// [SerializeField] float abilityRange = 10f;

		public override ISpecialAbility AddComponentTo (GameObject gameObjectToAttachTo)
		{
			var behaviorComponent = gameObjectToAttachTo.AddComponent<PowerAttackBehavior>();
			// pass this whole config into the behavior script
			behaviorComponent.SetConfig(this);
			return behaviorComponent;
		}
	}
}
