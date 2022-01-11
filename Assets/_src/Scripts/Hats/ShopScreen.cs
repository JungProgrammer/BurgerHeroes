using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BurgerHeroes.Event;
using BurgerHeroes.UI;
using Sirenix.OdinInspector;
using System;

namespace BurgerHeroes.Hats
{
    public class ShopScreen : SerializedMonoBehaviour
    {
        private const string HAT_PLAYER_PREFS_PREFIX = "hatBought_";

        [SerializeField, AssetsOnly, Required]
        private GameEvent _closeShop;

        [SerializeField, ChildGameObjectsOnly, Required]
        private GameObject _buyHatButton;

        [SerializeField, Required]
        private CoinsView _coins;

        [SerializeField] private Dictionary<HatVariants, ShopScreenHatButton> _hatButtons;

        private List<HatVariants> _lockedHats = new List<HatVariants>();

        private void Awake() {
            FindLockedHats();

            _hatButtons[HatVariants.Default].Activate();
            _lockedHats.Remove(HatVariants.Default);

            CheckBuyButton();
        }

        private void FindLockedHats() {
            foreach(HatVariants hat in Enum.GetValues(typeof(HatVariants))) {
                if (CheckIfBought(hat))
                    _hatButtons[hat].Activate();
                else _lockedHats.Add(hat);
            }
        }

        private bool CheckIfBought(HatVariants hat) {
            bool result = false;
            if (PlayerPrefs.GetInt(HAT_PLAYER_PREFS_PREFIX + hat.ToString(), 0) != 0)
                result = true;
            return result;
        }

        private void CheckBuyButton() {
            if (_lockedHats.Count == 0) {
                _buyHatButton.SetActive(false);
            }
        }

        public void CloseShopScreen() {
            _closeShop.Raise();
        }

        private HatVariants PopRandomHat() {
            int popItemIndex = UnityEngine.Random.Range(0, _hatButtons.Count);
            HatVariants result = _lockedHats[popItemIndex];
            _lockedHats.RemoveAt(popItemIndex);
            return result;
        }

        public void BuyHat() {
            if (PlayerPrefs.GetInt("CoinsCount", 0) >= 1000) {
                HatVariants boughtHat = PopRandomHat();
                _hatButtons[boughtHat].Activate();
                Debug.Log(boughtHat);
                PlayerPrefs.SetInt(HAT_PLAYER_PREFS_PREFIX + boughtHat.ToString(), 1);
                _coins.ReduceCoins(1000);
                CheckBuyButton();
            }
        }
    }
}
