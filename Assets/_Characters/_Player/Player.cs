using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;
using RPG.Weapons;

namespace RPG.Characters
{
	public class Player : MonoBehaviour, IDamageable 
	{
		// [SerializeField] int enemyLayer = 9;
		// TODO: make navMeshAgent stopping distance = WEAPON attackRadius
		[SerializeField] float unarmedAttackRadius = 2f;
		[SerializeField] float baseDamage = 10f;
		[SerializeField] float globalCooldown = 1f;
		float lastHitTime = 0f;

		[SerializeField] float maxHealthPoints = 100f;
		float currentHealthPoints;
		public bool isAlive = true;
		public bool isAttacking = false;
		// TODO: for tabbing through enemies: GameObject currentTarget;
		CameraRaycaster cameraRaycaster;
		[SerializeField] AnimatorOverrideController animatorOverrideController;
		private Animator animator;
		// Weapon Related:
		[SerializeField] Weapon currentWeapon;
		[SerializeField] GameObject weaponSocket;
		GameObject weaponHand;
		//Special abilities:
		Energy energy;
		// TODO: serialized only for debugging 
		[SerializeField] SpecialAbilityConfig[] abilities;

		public float healthAsPercentage 
		{
			get 
			{
				return currentHealthPoints / maxHealthPoints;
			}
		}

		// use - void IDamageable.TakeDamage(float damage) - in order to only be useable through the interface, and not by other classes
		// TODO: add value to params to scale dmg as level increases
		public void TakeDamage(float damage) 
		{
			// Clamp - clamps a value between a min and max float value
			currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
			// if (currentHealthPoints <= 0) { Destroy(gameObject); }	
		}

		void OnMouseClick(Enemy enemy)
		{
			//TODO: Add similar to ENERGY script
			if (Input.GetMouseButtonDown(0) && WithinRange(enemy))
			{
				AttackTarget(enemy);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				// TODO: make non-specific to ability[0]
				UseSpecialAbility(1, enemy);
			}
		}

		void Start() 
		{
			RegisterMouseClick();
			SetCurrentHealth();
			DrawWeapon();
			OverrideAnimator();
			energy = GetComponent<Energy>();
			AttachAbilityComponents();
			ResetSpecialAbilityCooldowns();
		}

		void Update () 
		{
			if (!isAlive) { return; }

			if (currentHealthPoints == 0)
			{
				// TODO: refactor to DEATH method, play death noise; make ghost, etc... 
				isAlive = false;
			}
			SwitchWeapon();
		}

		private void RegisterMouseClick() 
		{
			cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
			cameraRaycaster.mouseOverEnemy += OnMouseClick;
			// cameraRaycaster.mouseOverEnemy += OnRightClick;

		}

		private void SetCurrentHealth() 
		{
			currentHealthPoints = maxHealthPoints;
		}

		private void OverrideAnimator() 
		{
			animator = GetComponent<Animator>();
			animator.runtimeAnimatorController = animatorOverrideController;
			animatorOverrideController["Default Attack"] = currentWeapon.GetAttackAnimClip();
		}


		private void DrawWeapon() 
		{
			// TODO: add arming anim + delayed instantiation
			var weaponPrefab = currentWeapon.GetWeaponPrefab();
			var weapon = Instantiate(weaponPrefab, weaponSocket.transform);
			weapon.transform.localPosition = currentWeapon.weaponGripTransform.localPosition;
			weapon.transform.localRotation = currentWeapon.weaponGripTransform.localRotation;
		}

	// TODO: switch weapon while in game
		private void SwitchWeapon()
		{
			if (!currentWeapon)
			{
				DrawWeapon();
			}
		}

		//TODO: add param for range, in order to pass in melee range of player, or ability range
		private bool WithinRange(Enemy target)
		{
			float distanceToTarget = (target.transform.position - transform.position).magnitude;		
			return distanceToTarget <= currentWeapon.GetAttackRadius();
			// return distanceToTarget <= range;

		}

		private bool CheckWeaponCooldown(Weapon weapon)
		{
			return Time.time - lastHitTime > weapon.GetAttackCooldown();
		}

		private void AttackTarget(Enemy target) 
		{
			// TODO: don't need script - already filtering for previously?
			var targetScript = target.GetComponent<Enemy>();
			if (CheckWeaponCooldown(currentWeapon))
			{
				animator.SetTrigger("Attack");
				targetScript.TakeDamage(baseDamage);
				lastHitTime = Time.time;
			}
		}

		// Special Abilities:

		private void AttachAbilityComponents()
		{
			var i = 0;
			while(i < abilities.Length)	
			{
				abilities[i].AddComponentTo(gameObject);
				i++;
			}
		}

		private void ResetSpecialAbilityCooldowns()
		{
			var i = 0;
			while(i < abilities.Length)
			{			
				abilities[i].lastHitTime = 0f;
				i++;
			}		
		}

		private void UseSpecialAbility(int index, Enemy enemy)
		{
			if (energy.IsEnergyAvailable(abilities[index].GetEnergyCost()) && IsAbilityAvailable(abilities[index]) && WithinAbilityRange(enemy, abilities[index]))
			// if (energy.IsEnergyAvailable(abilities[index].GetEnergyCost()) && IsAbilityAvailable(abilities[index]) && WithinRange(enemy, abilities[index]))
				{
					energy.UpdateEnergyPoints(abilities[index].GetEnergyCost());
					var abilityParams = new AbilityUseParams(enemy, baseDamage);
					abilities[index].Use(abilityParams);
					abilities[index].lastHitTime = Time.time;
				}
		}

		private bool WithinAbilityRange(Enemy target, SpecialAbilityConfig ability)
		{
			float distanceToTarget = (target.transform.position - transform.position).magnitude;		
			return distanceToTarget <= ability.GetRange();
		}

		private bool IsAbilityAvailable(SpecialAbilityConfig ability)
		{
			return Time.time - ability.lastHitTime > ability.GetCooldown();
		}



		// testing arm and disarm anims

		// private void ArmForCombat() 
		// {
		// 	if(isAttacking true) 
		// 	{
				// base off of lastHitTime or distanceTo enemy?
		// 	}
		// }


	}
}
