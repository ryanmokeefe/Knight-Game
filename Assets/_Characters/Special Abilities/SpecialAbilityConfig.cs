using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	// param struct to use when calling .Use() on ANY specialAbility
	public struct AbilityUseParams
	{
		public IDamageable target;
		public float baseDamage;
		// Constructor: same name as struct
		public AbilityUseParams(IDamageable target, float baseDamage)
		{
			this.target = target;
			this.baseDamage = baseDamage;
		}
	}

	public abstract class SpecialAbilityConfig : ScriptableObject 
	{
		[Header("Special Ability General")]
		
		// TODO: consider making public or adding setter in order to Set upon new level
		[SerializeField] float energyCost = 40f;
		[SerializeField] float cooldownTime = 4f;
		[SerializeField] float abilityRange = 10f;
		public float lastHitTime = 0f;
		protected ISpecialAbility behavior;

// TODO: use abstract method in order to set specific CD, Range, Cost...? 
		abstract public void AddComponentTo (GameObject gameObjectToAttachTo);


		public void Use(AbilityUseParams useParams)
		{
			behavior.Use(useParams);
		}

		public float GetEnergyCost()
		{
			return energyCost;
		}		

		public float GetCooldown()
		{
			return cooldownTime;
		}

		public float GetRange()
		{
			return abilityRange;
		}

	// Setters:
		public void SetEnergyCost(float energy)
		{
			this.energyCost = energy;
		}		

		public void SetCooldown(float cooldown)
		{
			this.cooldownTime = cooldown;
		}

		public void SetRange(float range)
		{
			this.abilityRange = range;
		}

	}

	public interface ISpecialAbility 
	{
		void Use(AbilityUseParams useParams);
	}
}
