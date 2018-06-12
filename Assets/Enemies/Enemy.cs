using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float moveRadius = 5f;
	[SerializeField] float outRunDistance = 15f;

	[SerializeField] float attackRadius = 4f;
	[SerializeField] float damagePerShot = 5f;
	[SerializeField] float secondsBetweenShots = 0.5f;

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

///

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
			InvokeRepeating("SpawnProjectile", 0, secondsBetweenShots); 
		}
		if (distanceToPlayer > attackRadius)
		{
			isAttacking = false;
			CancelInvoke("SpawnProjectile");
		}
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

	void SpawnProjectile()
	{
		GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
		Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
		projectileComponent.SetDamage(damagePerShot);
		// get the unit vector to player
		Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized;
		float projectileSpeed = projectileComponent.projectileSpeed;
		newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
	print("Projectile spawned");

	}

	void OnDrawGizmos()
		{
			// black for move
		
			// Gizmos.color = Color.black;
			// Gizmos.DrawWireSphere(transform.position, moveRadius);
		
			// red for shoot/attack
			Gizmos.color = new Color(255f, 0f, 0f, .4f);
			Gizmos.DrawWireSphere(transform.position, attackRadius);
		
			// blue for outrun
			// Gizmos.color = new Color(0f, 0f, 255f, .4f);
			// Gizmos.DrawWireSphere(transform.position, outRunDistance);

		}
}
