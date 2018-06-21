﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using RPG.Weapons;
using RPG.CameraUI;


namespace RPG.Characters
{
	public class Enemy : MonoBehaviour, IDamageable 
	{

		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] float moveRadius = 5f;
		[SerializeField] float outRunDistance = 15f;

		[SerializeField] float attackRadius = 4f;
		[SerializeField] float damagePerShot = 5f;
		[SerializeField] float secondsBetweenShots = 0.5f;
		// fixed enemy aim
		[SerializeField] Vector3 aimOffset = new Vector3(0f, 1f, 0f);

		[SerializeField] GameObject projectileToUse;
		[SerializeField] GameObject projectileSocket;

		bool isAttacking = false;
		float currentHealthPoints;
		public bool isAlive = true;

		EnemyAICharacterControl AIControl = null;
		GameObject player = null;
		GameObject origin;


			public void TakeDamage(float damage) 
		{
			currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
			if (currentHealthPoints <= 0) { Destroy(gameObject); }	
		}

		public float healthAsPercentage 
		{
			get 
			{
				return currentHealthPoints / maxHealthPoints;
			}
		}

		void Start() 
		{
			currentHealthPoints = maxHealthPoints;
			origin = new GameObject("Origin");
			origin.transform.position = transform.position;
			player = GameObject.FindGameObjectWithTag("Player");
			AIControl = GetComponent<EnemyAICharacterControl>();
		}

		
		///

		// public void SetTarget(GameObject target) 
		// {
		// 	currentTarget = target;
		// }

		// public void ClearTarget()
		// {
		// 	currentTarget = null;
		// }

		// public bool isAttacking
		// {
		// 	get 
		// 	{
		// 		return currentTarget != null;
		// 	}
		// }

		// Update is called once per frame
		void Update () 
		{

			float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

			if (!isAlive) { return; }

			if (currentHealthPoints == 0)
			{
				isAlive = false;
		// TODO: play death noise; 
			}

			// Attack and Aggro radius
			if (distanceToPlayer <= attackRadius && !isAttacking)
			{
				isAttacking = true;
				// TODO: slow speed
				InvokeRepeating("FireProjectile", 0, secondsBetweenShots); 
			}
			if (distanceToPlayer <= attackRadius)
			{
				gameObject.transform.LookAt(player.transform);
			}
			if (distanceToPlayer > attackRadius)
			{
				isAttacking = false;
				CancelInvoke("FireProjectile");
			}
			// TODO: fix facing direction when targeted BUT not moving toward yet (ranged)
			if (distanceToPlayer <= moveRadius)
			{

				AIControl.SetTarget(player.transform);
			}
			if (distanceToPlayer > outRunDistance && AIControl.target != origin.transform)
			{
		// TODO: origin not resetting if player enters ATTACK Radius (resets if entering Move radius only)
		// EDIT: possible glitch with unity object correctly reading script
				AIControl.SetTarget(origin.transform);
				print(gameObject.name + " returning");
			}

			
		}

		// TODO: seperate character firing logic into seperate, reusable class
		void FireProjectile()
		{
			GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
			Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
			projectileComponent.SetDamage(damagePerShot);
			projectileComponent.SetShooter(gameObject);

		// TODO: seperate 
			// get the unit vector to player, set projectile velocity
			Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
			float projectileSpeed = projectileComponent.projectileSpeed;
			newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;

		}

		void OnDrawGizmos()
			{
				// black for move
			
				Gizmos.color = Color.black;
				Gizmos.DrawWireSphere(transform.position, moveRadius);
			
				// red for shoot/attack
				Gizmos.color = new Color(255f, 0f, 0f, .4f);
				Gizmos.DrawWireSphere(transform.position, attackRadius);
			
				// blue for outrun
				Gizmos.color = new Color(0f, 0f, 255f, .4f);
				Gizmos.DrawWireSphere(transform.position, outRunDistance);

			}
	}
}