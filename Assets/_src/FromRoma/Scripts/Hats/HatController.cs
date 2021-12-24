using UnityEngine;
using System;

namespace BurgerHeroes.Hats
{
    public class HatController : MonoBehaviour
    {
        [SerializeField] private Transform hatContainer;
        [SerializeField] private HatCollection hatCollection;

        private GameObject _currentHat;

        private void Start() {
            string currentHatName = PlayerPrefs.GetString("Hat", "Default");
            SpawnHat((HatVariants)Enum.Parse(typeof(HatVariants), currentHatName));
        }

        private void SpawnHat(HatVariants hat) {
            _currentHat = Instantiate(hatCollection.GetHatPrefab(hat), hatContainer);
        }

        public void OnChangeHat(object hatType) {
            Destroy(_currentHat);
            SpawnHat((HatVariants)hatType);
        }
    }
}
