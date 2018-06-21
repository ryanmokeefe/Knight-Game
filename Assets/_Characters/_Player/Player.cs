using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;
using RPG.Weapons;

namespace RPG.Characters
{
	public class Player : MonoBehaviour, IDamageable {

		[SerializeField] int enemyLayer = 9;
		[SerializeField] float unarmedAttackRadius = 2f;
		[SerializeField] float damagePerHit = 10f;
		[SerializeField] float globalCooldown = 1f;


		float lastHitTime = 0f;

		[SerializeField] float maxHealthPoints = 100f;
		float currentHealthPoints;
		public bool isAlive = true;
		public bool isAttacking = false;
		// GameObject currentTarget;
		CameraRaycaster cameraRaycaster;
		[SerializeField] AnimatorOverrideController animatorOverrideController;
		private Animator animator;
		// Weapon Related:
		[SerializeField] Weapon currentWeapon;
		[SerializeField] GameObject weaponSocket;
		GameObject weaponHand;

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
			if (Input.GetMouseButtonDown(0))
			{
				// var enemy = raycastHit.collider.gameObject;
				if (WithinRange(enemy))
				{
					AttackTarget(enemy);
				}
				// currentTarget = enemy;
			}
		}

		void Start() 
		{
			RegisterMouseClick();
			SetCurrentHealth();
			DrawWeapon();
			OverrideAnimator();
			
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

		private bool WithinRange(Enemy target)
		{
			float distanceToTarget = (target.transform.position - transform.position).magnitude;		
			return distanceToTarget <= currentWeapon.GetAttackRadius();
			
		}

		private bool CheckWeaponCooldown(Weapon weapon)
		{
			return Time.time - lastHitTime > weapon.GetAttackCooldown();
		}

		private void AttackTarget(Enemy target) 
		{
			var targetScript = target.GetComponent<Enemy>();
			if (CheckWeaponCooldown(currentWeapon))
			{
				animator.SetTrigger("Attack");
				targetScript.TakeDamage(damagePerHit);
				lastHitTime = Time.time;
			}
		}



		// testing arm and disarm anims

		// private void ArmForCombat() 
		// {
		// 	if(isAttacking true) 
		// 	{

		// 	}
		// }


	}
}
