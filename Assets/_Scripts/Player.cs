using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

	[SerializeField] int enemyLayer = 9;
	[SerializeField] float attackRadius = 2f;
	[SerializeField] float damagePerHit = 10f;
	[SerializeField] float attackCooldown = 0.5f;
	float lastHitTime = 0f;

	[SerializeField] float maxHealthPoints = 100f;
	float currentHealthPoints;
	public bool isAlive = true;
	GameObject currentTarget;
	CameraRaycaster cameraRaycaster;
	


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

	void OnMouseClick(RaycastHit raycastHit, int layerHit)
	{
		if (layerHit == enemyLayer)
		{
			var enemy = raycastHit.collider.gameObject;
			// print("Clicked enemy " + enemy);

			// check enemy/attack range

			if ((enemy.transform.position - transform.position).magnitude > attackRadius)
			{
				return;
			}

			currentTarget = enemy;
			var enemyComponent = enemy.GetComponent<Enemy>();
			if (Time.time - lastHitTime > attackCooldown)
			{
				enemyComponent.TakeDamage(damagePerHit);
				lastHitTime = Time.time;
			}
		}
	}

///


	void Start() 
	{
		currentHealthPoints = maxHealthPoints;
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
	}

	void Update () {
		if (!isAlive) { return; }

		if (currentHealthPoints == 0)
		{
			isAlive = false;
			// TODO: play death noise; make ghost... 
		}

	}
}
