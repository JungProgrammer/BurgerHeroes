using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BurgerHeroes.Event;

namespace BurgerHeroes.Hats
{
    public class ShopScreenHatButton : MonoBehaviour
    {
        [SerializeField] private HatVariants _hatVariant;
        [SerializeField] private GameEventWithParameter _hatChanged;
        [SerializeField] private GameObject _lock;

        private bool _isActive = false;

        public void Activate() {
            _isActive = true;
            _lock.SetActive(false);
        }

        public void ChangeHat() {
            if (_isActive) {
                _hatChanged.Raise(_hatVariant);
                PlayerPrefs.SetString("Hat", _hatVariant.ToString());
            }
        }
    }
}
