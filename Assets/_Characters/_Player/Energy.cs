using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Characters
{
	public class Energy : MonoBehaviour {

		[SerializeField] RawImage energyBarImage;
		[SerializeField] float maxEnergyPoints = 200f;
		[SerializeField] float regenPerSecond = 10f;
		float currentEnergyPoints;

		public float energyAsPercentage 
		{
			get 
			{
				return currentEnergyPoints / maxEnergyPoints;
			}
		}

		void Start () 
		{
			SetCurrentEnergy();
		}

		void Update ()
		{
			if (currentEnergyPoints < maxEnergyPoints) 
			{ 
				RegenEnergy(); 
				UpdateEnergyBar();	
			}
		
			print("Current Energy Points: " + currentEnergyPoints);
		}

		public void SetCurrentEnergy()
		{
			currentEnergyPoints = maxEnergyPoints;
		}

		public bool IsEnergyAvailable(float energyCost)
		{
			return energyCost <= currentEnergyPoints;
		}

		public void UpdateEnergyPoints(float energyCost)
		{	
			float newEnergyPoints = currentEnergyPoints - energyCost;
			currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0f, maxEnergyPoints);
			UpdateEnergyBar();
		}

		private void UpdateEnergyBar()
		{
			// TODO: remove magic numbers
			float xValue = -(energyAsPercentage / 2f) - 0.5f;
            energyBarImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
		}

		// IEnumerator RegenEnergy()
		// {
		// 	float newEnergyPoints = currentEnergyPoints + regenPerSecond;
		// 	currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0f, maxEnergyPoints);
		// 	UpdateEnergyBar();
		// 	print("TEST" + currentEnergyPoints);
		// 	yield return new WaitForSecondsRealtime(1);

		// }

		private void RegenEnergy()
		{
			float newEnergyPoints = currentEnergyPoints + (regenPerSecond * Time.deltaTime);
			currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0f, maxEnergyPoints);
						print("TEST" + currentEnergyPoints);
		}

	}
}
