using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Food;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Levels
{
    public class LevelRecipe : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly, Required]
        private Dictionary<IngredientKey, int> _needLevelIngredients = new Dictionary<IngredientKey, int>();


        public Dictionary<IngredientKey, int> GetNeedLevelIngredients()
        {
            return _needLevelIngredients;
        }
    }   
}
