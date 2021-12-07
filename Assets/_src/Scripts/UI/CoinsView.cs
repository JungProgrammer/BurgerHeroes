using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Analytics;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


namespace BurgerHeroes.UI
{
    public class CoinsView : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly, Required]
        private TextMeshProUGUI _coinsCountText;


        [SerializeField, Required] 
        private Animator _animator;


        private const string ScaleCoinsPanelTrigger = "ScaleCoinsPanelTrigger";
        

        private int _collectedCoinsCount;


        public int CollectedCoinsCount => _collectedCoinsCount;


        private void Awake()
        {
            LoadCoinsTextInMenu();
            _collectedCoinsCount = 0;
        }


        public void LoadCoinsTextInMenu()
        {
            int currentAllCoinsCount = PlayerPrefs.GetInt("CoinsCount", 0) + _collectedCoinsCount;
            
            PlayerPrefs.SetInt("CoinsCount", currentAllCoinsCount);
            
            _coinsCountText.text = currentAllCoinsCount.ToString();

            AmplitudeManager.Instance.SetCurrentSoft();
        }


        public void NullifyCoinsText()
        {
            _collectedCoinsCount = 0;
            _coinsCountText.text = "0";
        }


        public void IncrementCoinsCount()
        {
            _collectedCoinsCount++;
            _coinsCountText.text = _collectedCoinsCount.ToString();
            _animator.SetTrigger(ScaleCoinsPanelTrigger);
        }
    }   
}
