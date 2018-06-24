using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public abstract class SpecialAbilityConfig : ScriptableObject 
	{
		[Header("Special Ability General")]
		
		[SerializeField] public float energyCost = 10f;
		[SerializeField] public float cooldownTime = 4f;
		[SerializeField] public float abilityRange = 10f;
		public float lastHitTime = 0f;

		// dmg multiplier can be added later

		abstract public ISpecialAbility AddComponentTo (GameObject gameObjectToAttachTo);
	}
}
