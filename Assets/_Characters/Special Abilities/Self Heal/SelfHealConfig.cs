using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu(menuName = ("RPG/Special Abilities/Self Heal"))]

	public class SelfHealConfig : SpecialAbilityConfig 
	{

		[Header("Self Heal Specific")]

		[SerializeField] float healAmount = 100f;

		public override void AddComponentTo (GameObject gameObjectToAttachTo)
		{
			var behaviorComponent = gameObjectToAttachTo.AddComponent<SelfHealBehavior>();
			behaviorComponent.SetConfig(this);
			behavior = behaviorComponent;
		}

		public float GetHealth()
		{
			return healAmount;
		}

	}
}
