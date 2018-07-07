using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class GoblinAnim : MonoBehaviour {

		// 10 - run, 11 - walk // melee rad - 2
		[SerializeField] float animSpeed = 2f;
		Enemy enemyScript = null;
		Animation animControl = null;
		Vector3 currentPos;
		Vector3 newPos;
		bool isAttacking = false;
		string attack;
		float attackRadius;
		float pauseTime;
		float countdown = 0f;


		// Use this for initialization
		void Start () {
			enemyScript = GetComponent<Enemy>();
			currentPos = gameObject.transform.position;
			animControl = GetComponent<Animation>();
			animControl["run"].speed = animSpeed;
			attackRadius = enemyScript.GetAttackRadius();

			if (attackRadius <= 2)
			{
				attack = "attack3";
			}
			else 
			{
				attack = "attack1";
			}
		}
		
		// Update is called once per frame
		void Update () {
			CheckForMovement();
			GetPauseTime();

		}

		void CheckForMovement()
		{
			newPos = gameObject.transform.position;
			if (newPos != currentPos)
			{
				animControl.Play("run");
				currentPos = newPos;
			}
			else if (CheckForAttacking())
			{
				animControl.Stop("run");
				// utilize PauseTime from enemy (updated)
				// animControl.Play(attack);
				CycleAttackWithPause();
			}
			else
			{
				animControl.Play("combat_idle");
			}
		}

		bool CheckForAttacking()
		{
			isAttacking = enemyScript.IsCurrentlyAttacking();
			return isAttacking;
			
		}

		void GetPauseTime()
		{
			pauseTime = enemyScript.GetPauseTime();
		}

		void CycleAttackWithPause()
		{
			if (countdown <= 0)
			{
				animControl.Play(attack);
				float animLength = animControl[attack].length;
				print("AnimLength is: " + animLength);
	// // Time.time vs Time.deltaTime ??
				float timeWhenAnimIsDonePlaying = animLength - Time.deltaTime;
				if (timeWhenAnimIsDonePlaying <= 0)
				{
				// sets countdown too quickly, not playing anim - wait til anim is through
					countdown = pauseTime;
				}
				print("Just attacked, CD is: " + countdown);
			}
			else
			{
				animControl.Play("combat_idle");
	// // Time.time vs Time.deltaTime ??
				countdown = countdown - Time.deltaTime;
				print("Waiting to attack, CD is: " + countdown);
			}
		}
	}
}
