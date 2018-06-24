using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class PowerAttackBehavior : MonoBehaviour, ISpecialAbility 
	{
		PowerAttackConfig config;

		public void SetConfig(PowerAttackConfig newConfig)
		{
			this.config = newConfig;
		}


		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void Use()
		{

		}
	}	
}
