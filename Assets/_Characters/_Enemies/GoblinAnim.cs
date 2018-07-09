using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class GoblinAnim : MonoBehaviour {

		// 10 - run, 11 - walk // melee rad - 2
		[SerializeField] float animSpeed = 2f;
		[SerializeField] float animAttackSpeed = 1f;
		[SerializeField] string attack;
		Enemy enemyScript = null;
		Animation animControl = null;
		Vector3 currentPos;
		Vector3 newPos;
		bool isAttacking = false;
		bool isAlive = true;
		float attackRadius;
		float pauseTime;
		float animLength;
		
		float lastHitTime = 0f;
		float lastPlayTime = 0f;


		void Start () 
		{
			enemyScript = GetComponent<Enemy>();
			currentPos = gameObject.transform.position;
			animControl = GetComponent<Animation>();
			animControl["run"].speed = animSpeed;
			animControl[attack].speed = animAttackSpeed;
			attackRadius = enemyScript.GetAttackRadius();
			animLength = animControl[attack].length;
		}
		
		void Update () 
		{
			CheckForMovement();
			GetPauseTime();
			if (!enemyScript.isAlive)
			{
				isAlive = false;
			}
			if (!isAlive)
			{
				KillGoblin();
			}
			// print("Script still alive");
			
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

		bool ReadyToHit()
		{
			return Time.time - lastHitTime > pauseTime;
		}

		bool ReadyToPlayAnim()
		{
			return Time.time - lastPlayTime > animLength;
		}


		void CycleAttackWithPause()
		{
			if (ReadyToHit() && ReadyToPlayAnim())
			{
				animControl.Play(attack);
				lastPlayTime = Time.time;
				lastHitTime = Time.time;
			}
			else if (ReadyToPlayAnim())
			{
				animControl.Play("combat_idle");
			}
		}

		public void KillGoblin()
		{
			animControl.Stop();
			animControl.Play("death");
			Destroy(this);
		}

	}
}
