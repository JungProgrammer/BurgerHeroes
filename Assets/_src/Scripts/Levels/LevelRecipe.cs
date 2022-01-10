using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BurgerHeroes.Food;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Levels
{
    [DefaultExecutionOrder(-1)]
    public class LevelRecipe : SerializedMonoBehaviour
    {
        public static LevelRecipe Instance;


        [SerializeField, AssetsOnly, Required]
        private Dictionary<IngredientKey, int> _needLevelIngredients = new Dictionary<IngredientKey, int>();


        [SerializeField, ChildGameObjectsOnly, Required]
        private Transform _ingredientsHolder;


        public Transform IngredientsHolder => _ingredientsHolder;


        private void Awake()
        {
            Instance = this;
        }


        public Dictionary<IngredientKey, int> GetNeedLevelIngredients()
        {
            return _needLevelIngredients;
        }


        public IngredientKey GetRandomIngredientKey()
        {
            IngredientKey ingredientKey = _needLevelIngredients.ElementAt(Random.Range(0, _needLevelIngredients.Count)).Key;

            return ingredientKey;
        }
    }   
}
