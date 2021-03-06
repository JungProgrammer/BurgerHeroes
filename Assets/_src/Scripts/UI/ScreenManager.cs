using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Food;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.UI
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly, Required]
        private MainMenu _mainMenu;


        [SerializeField, ChildGameObjectsOnly, Required]
        private PostGameplayWindow _postGameplayWindow;


        [SerializeField, ChildGameObjectsOnly, Required]
        private CoinsView _coinsPanel;


        [SerializeField, ChildGameObjectsOnly, Required]
        private GameObject _defeatScreen;

        [SerializeField, ChildGameObjectsOnly, Required]
        private GameObject _shopScreen;

        private void Awake()
        {
            _coinsPanel.gameObject.SetActive(true);
            _mainMenu.gameObject.SetActive(true);
        }


        public void StartGameplay()
        {
            _mainMenu.gameObject.SetActive(false);
            _postGameplayWindow.gameObject.SetActive(false);
        }


        public void OpenMenu()
        {
            _mainMenu.gameObject.SetActive(true);
            _postGameplayWindow.gameObject.SetActive(false);
            _coinsPanel.gameObject.SetActive(true);
        }


        public void OpenPostGameplayWindow()
        {
            _postGameplayWindow.gameObject.SetActive(true);
            _coinsPanel.gameObject.SetActive(false);
        }

        public void OpenDefeatScreen() {
            _coinsPanel.gameObject.SetActive(false);
            _defeatScreen.SetActive(true);
        }

        public void OpenShopScreen() {
            _mainMenu.gameObject.SetActive(false);
            _shopScreen.SetActive(true);
        }

        public void CloseShopScreen() {
            _mainMenu.gameObject.SetActive(true);
            _shopScreen.SetActive(false);
        }
    }   
}
