using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System;

namespace BurgerHeroes.Hats
{
    public class ShopScreenCurrentHat : SerializedMonoBehaviour
    {
        [SerializeField] private Image _currentHatImage;
        [SerializeField] private Dictionary<HatVariants, Sprite> _hatSprites;

        private void Start() {
            string currentHatName = PlayerPrefs.GetString("Hat", "Default");
            SpawnHat((HatVariants)Enum.Parse(typeof(HatVariants), currentHatName));
        }

        private void SpawnHat(HatVariants hat) {
            _currentHatImage.sprite = _hatSprites[hat];
        }

        public void OnHatChanged(object hat) {
            SpawnHat((HatVariants)hat);
        }
    }
}