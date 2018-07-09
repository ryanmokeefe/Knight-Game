using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;
using RPG.Weapons;
using UnityEngine.SceneManagement;

namespace RPG.Characters
{
	public class Player : MonoBehaviour, IDamageable 
	{
		// TODO: make navMeshAgent stopping distance = WEAPON attackRadius
		[SerializeField] float unarmedAttackRadius = 2f;
		[SerializeField] float aggroRadius = 10f;
		[SerializeField] float baseDamage = 10f;
		[SerializeField] float globalCooldown = 1f;
		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] float maxExpPoints = 1000f;
		[SerializeField] Light targetSpotlight;
		
		// TODO: add hit sounds to weapons and abilities
		[SerializeField] AudioClip[] damageSounds;
		[SerializeField] AudioClip[] deathSounds;
		
		[SerializeField] AnimatorOverrideController animatorOverrideController;
		CameraRaycaster cameraRaycaster;
		private Animator animator;
		private AudioSource audioSource;
		
		Energy energy;
		IDamageable player;
		Enemy currentEnemy = null;
		float currentHealthPoints;
		float lastHitTime = 0f;
		const string attackTrigger = "Attack";
		const string deathTrigger = "Death";
		public bool isAlive = true;
		public bool isAttacking = false;

		// Weapon Related:
		[SerializeField] Weapon currentWeapon;
		[SerializeField] GameObject weaponSocket;
		GameObject weaponHand;
		//Special abilities:
		[SerializeField] SpecialAbilityConfig[] abilities;


		public AnimatorOverrideController GetAnimOverrideController()
		{
			return animatorOverrideController;
		}

		public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

		void OnMouseClick(Enemy enemy)
		{
			// TODO: destroy light when enemy dies/add XP gain
			if (Input.GetMouseButtonDown(0))
			{
				currentEnemy = enemy;
				TargetEnemy(enemy);
			}
			else if (Input.GetMouseButtonDown(1) && WithinRange(currentEnemy))
			{
				AttackTarget();
			}
		}

		void Start() 
		{
			player = this;
			energy = GetComponent<Energy>();
			audioSource = GetComponent<AudioSource>();
			RegisterMouseClick();
			SetCurrentHealth();
			DrawWeapon();
			OverrideAnimator();
			AttachAbilityComponents();
			ResetSpecialAbilityCooldowns();
		}

		void Update () 
		{
			print(isAlive);
			print("Current enemy is: " + currentEnemy);
			if (healthAsPercentage < Mathf.Epsilon) { isAlive = false; }
			if (!isAlive) { return; }
			if (isAlive)
			{
				CheckForAbilityKeyDown();
				CheckForNullEnemy();
			}
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
		}


		// TODO: add value to params to scale dmg as level increases
		public void TakeDamage(float damage) 
		{
			bool playerIsDead = currentHealthPoints - damage <= 0;
			ReduceHealthPoints(damage);
			if (playerIsDead) 
			{
				// // re-enable enemy script as well when resuming player death:
				// StartCoroutine(KillPlayer());
			}
		}

		public void ReduceHealthPoints(float damage)
		{
			currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);	
			audioSource.clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
			audioSource.Play();
		}

		public void Heal(float amount)
		{
			currentHealthPoints = Mathf.Clamp(currentHealthPoints + amount, 0f, maxHealthPoints);	
		}

		IEnumerator KillPlayer()
		{
			audioSource.clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
			audioSource.Play();
			animator.SetTrigger(deathTrigger);
			yield return new WaitForSecondsRealtime(audioSource.clip.length);
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
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

		private void TargetEnemy(Enemy target)
		{
			// TODO: find alternative to spotlight (multiple lights currently able to appear)

			Vector3 lightPosition = target.gameObject.transform.position + new Vector3(0, 2, 0);

			Instantiate(targetSpotlight, lightPosition, targetSpotlight.transform.rotation, target.gameObject.transform);
			transform.LookAt(currentEnemy.transform);
		}

		private void CheckForNullEnemy()
		{
			if (currentEnemy == null)
			{
				Vector3 origin = gameObject.transform.position;
				float radius = aggroRadius;
				RaycastHit[] targets = Physics.SphereCastAll(origin, radius, origin, radius);
				foreach(RaycastHit hit in targets)
				{
					Enemy enemyToTarget = hit.collider.gameObject.GetComponent<Enemy>();
					if (enemyToTarget)
					{
						currentEnemy = enemyToTarget;
						TargetEnemy(enemyToTarget);
						// ClearHighlights();
					}
				}
			}
		}

		private void ClearHighlights()
		{
			// TODO: clear extra lights
			Vector3 origin = gameObject.transform.position;
				float radius = aggroRadius;
				RaycastHit[] targets = Physics.SphereCastAll(origin, radius, origin, radius);
				foreach(RaycastHit hit in targets)
				{
					if (hit.collider.gameObject.name != currentEnemy.name)
					{
						var light = hit.collider.gameObject.transform.Find("TargetSpotlight(Clone)");
						print(light);
						Destroy(light.gameObject);
					}
				}
		}

		//TODO: add param for range, in order to pass in melee range of player, or ability range
		private bool WithinRange(Enemy target)
		{
			float distanceToTarget = (target.transform.position - transform.position).magnitude;		
			return distanceToTarget <= currentWeapon.GetAttackRadius();
		}

		private bool CheckWeaponCooldown(Weapon weapon)
		{
			return Time.time - lastHitTime > weapon.GetAttackCooldown();
		}

		private void AttackTarget() 
		{
			if (CheckWeaponCooldown(currentWeapon))
			{
				animatorOverrideController["Default Attack"] = currentWeapon.GetAttackAnimClip();
				animator.SetTrigger(attackTrigger);
				currentEnemy.TakeDamage(baseDamage);
				lastHitTime = Time.time;
			}
		}

		// Special Abilities:

		private void AttachAbilityComponents()
		{
			for (var i = 0; i < abilities.Length; i++)	
			{
				abilities[i].AddComponentTo(gameObject);
			}
		}

		private void ResetSpecialAbilityCooldowns()
		{
			for (var i = 0; i < abilities.Length; i++)
			{			
				abilities[i].lastHitTime = 0f;
			}		
		}

		private void CheckForAbilityKeyDown()
		{
			for (int index = 0; index < abilities.Length; index++)
			{
				if (Input.GetKeyDown(index.ToString()))
				{
					UseSpecialAbility(index);
				}
			}
		}

		private void UseSpecialAbility(int index)
		{
			if (energy.IsEnergyAvailable(abilities[index].GetEnergyCost()) && IsAbilityAvailable(abilities[index]))
				{
					if (abilities[index].TargetSelf())
					{
						energy.UpdateEnergyPoints(abilities[index].GetEnergyCost());
						var abilityParams = new AbilityUseParams(player, baseDamage, player);
						abilities[index].Use(abilityParams);
						abilities[index].lastHitTime = Time.time;
					}
					else if (WithinAbilityRange(abilities[index]))
					{
						energy.UpdateEnergyPoints(abilities[index].GetEnergyCost());
						var abilityParams = new AbilityUseParams(currentEnemy, baseDamage, player);
						abilities[index].Use(abilityParams);
						abilities[index].lastHitTime = Time.time;
					}
				}
		}

		private bool WithinAbilityRange(SpecialAbilityConfig ability)
		{
			float distanceToTarget = (currentEnemy.transform.position - transform.position).magnitude;		
			return distanceToTarget <= ability.GetRange();
		}

		private bool IsAbilityAvailable(SpecialAbilityConfig ability)
		{
			// TODO: switch to cached countdown timer
			return Time.time - ability.lastHitTime > ability.GetCooldown();
		}

		// void OnDrawGizmos()
		// 	{
		// 		// red for shoot/attack
		// 		Gizmos.color = new Color(255f, 0f, 0f, .4f);
		// 		Gizmos.DrawWireSphere(transform.position, unarmedAttackRadius);


		// 	}

	}
}
