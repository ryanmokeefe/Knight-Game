using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;


namespace RPG.Weapons
{
	[CreateAssetMenu(menuName = ("RPG/Weapon"))]
	
	public class Weapon : ScriptableObject {

		[SerializeField] GameObject weaponPrefab;
		[SerializeField] AnimationClip attackAnim;
		// TODO: add field for UI image + add image to UI
		[SerializeField] float attackCooldown = 1f;
		[SerializeField] float attackRadius = 2f;
		public Transform weaponGripTransform;

		public float GetAttackCooldown()
		{
			return attackCooldown;
		}

		public float GetAttackRadius()
		{
			return attackRadius;
		}

		public GameObject GetWeaponPrefab()
		{
			return weaponPrefab;
		}

		public AnimationClip GetAttackAnimClip()
		{
			return attackAnim;
		}

	}
}
