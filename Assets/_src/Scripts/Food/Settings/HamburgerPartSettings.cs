using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Food
{
    [CreateAssetMenu(fileName = "New HamburgerPartSettings", menuName = "Hamburger Part Settings")]
    public class HamburgerPartSettings : ScriptableObject
    {
        [SerializeField] 
        private IngredientKey _ingredientKey;
        
        
        [SerializeField]
        private float _height;
        
        
        [SerializeField]
        private float _speedDropPart;


        [SerializeField] 
        private float _timeToDisappearAfterThrow;


        public IngredientKey IngredientKey => _ingredientKey;
        public float Height => _height;
        public float SpeedDropPart => _speedDropPart;
        public float TimeToDisappearAfterThrow => _timeToDisappearAfterThrow;
    }   
}
