using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Characters
{
    [RequireComponent(typeof(Image))]
    public class PlayerHealthBar : MonoBehaviour
    {
        Player player;
        Image healthBarImage;

        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<Player>();
            healthBarImage = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            healthBarImage.fillAmount = player.healthAsPercentage;
        // TODO: add regen per second (decide whether to regen in combat?)
            // float xValue = -(player.healthAsPercentage / 2f) - 0.5f;
            // healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
    }
}
