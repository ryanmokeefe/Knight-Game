// void Update () 
// 	{

// 		float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

// 		if (distanceToPlayer <= moveRadius)
// 		{
// 			AIControl.SetTarget(player.transform);
// 			// continue to chase player while aggroed
// 			if(distanceToPlayer <= outRunDistance)
// 			{
// 				moveRadius = outRunDistance;
// 			}
// 		}
// 		else
// 		{
// 			AIControl.SetTarget(transform);
//             moveRadius = 5f;
// 		}
// 		if (distanceToPlayer <= attackRadius)
// 		{

// 			SpawnProjectile(); 
// 		}
		
// 	}

// 	void SpawnProjectile()
// 	{
// 		GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
// 		Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
// 		projectileComponent.damageCaused = damagePerShot;
// 		Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized;
// 		// float projectileSpeed = 10f;
// 		float projectileSpeed = projectileComponent.projectileSpeed;
// 		newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
// 	print("Projectile spawned");

// 	}






// Hi there, I'm having a small issue where only 11-12 projectiles spawn when the player enters the attack radius, then stop spawning. Everything else in void update works fine, any thoughts on where the issue may be?
