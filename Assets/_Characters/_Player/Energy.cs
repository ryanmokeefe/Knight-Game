using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;


namespace RPG.Characters
{
	public class Energy : MonoBehaviour {

		[SerializeField] RawImage energyBarImage;
		[SerializeField] float maxEnergyPoints = 200f;
		[SerializeField] float pointsPerHit = 10f;
		CameraRaycaster cameraRaycaster;
		float currentEnergyPoints;

		public float energyAsPercentage 
		{
			get 
			{
				return currentEnergyPoints / maxEnergyPoints;
			}
		}

		void Start () {
			SetCurrentEnergy();
			RegisterMouseClick();
		}
		
		void Update () {
		}



		public void SetCurrentEnergy()
		{
			currentEnergyPoints = maxEnergyPoints;
		}

		private void RegisterMouseClick() 
		{
			cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
			cameraRaycaster.mouseOverEnemy += MouseOverEnemy;
		}

		void MouseOverEnemy(Enemy enemy)
		{
			if(Input.GetMouseButtonDown(1))
			{
				float newEnergyPoints = currentEnergyPoints - pointsPerHit;
				currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0f, maxEnergyPoints);
				UpdateEnergyBar();
			}
		}

		private void UpdateEnergyBar()
		{
			float xValue = -(energyAsPercentage / 2f) - 0.5f;
            energyBarImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
		}

	}
}
