using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;


namespace RPG.Weapons
{
	[CreateAssetMenu(menuName = ("Weapon"))]
	public class Weapon : ScriptableObject {

		[SerializeField] GameObject weaponPrefab;
		[SerializeField] AnimationClip attackAnim;
		public Transform weaponGripTransform;


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
