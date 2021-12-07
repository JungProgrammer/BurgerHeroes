using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace BurgerHeroes.UI
{
    public class IngredientRecipe : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly, Required]
        private TextMeshProUGUI _ratioText;


        [SerializeField, ChildGameObjectsOnly, Required]
        private Image _checkMarkImage;
        
        
        private int _collectedCount;
        private int _neededCount;

        public int CollectedCount => _collectedCount;
        public int NeededCount => _neededCount;


        private void Awake()
        {
            _checkMarkImage.enabled = false;
        }


        public void SetRatioText(int collectedCount, int neededCount)
        {
            _collectedCount = collectedCount;
            _neededCount = neededCount;

            if (_collectedCount >= _neededCount)
            {
                // Чтобы отображение собранного колиства не было больше необходимого
                _collectedCount = _neededCount;
                _checkMarkImage.enabled = true;
            }

            _ratioText.text = _collectedCount.ToString() + "/" + _neededCount.ToString();
        }
    }   
}
