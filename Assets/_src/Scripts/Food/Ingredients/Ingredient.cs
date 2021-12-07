using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Food
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField, Required] 
        private IngredientAnimation _ingredientAnimation;
        
        
        [SerializeField] 
        private IngredientKey _ingredientKey;


        public IngredientKey IngredientKey => _ingredientKey;


        public void Collect()
        {
            _ingredientAnimation.StopAnimation();
        }
    }   
}
