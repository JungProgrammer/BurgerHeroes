using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Food;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.UI
{
    public class NeededRecipeIngredients : MonoBehaviour
    {
        [SerializeField, AssetsOnly, Required] 
        private CheeseIngredientRecipe _cheeseIngredient;


        [SerializeField, AssetsOnly, Required] 
        private LettuceIngredientRecipe _lettuceIngredient;


        [SerializeField, AssetsOnly, Required] 
        private MeatIngredientRecipe _meatIngredient;


        [SerializeField, AssetsOnly, Required] 
        private TomatoIngredientRecipe _tomatoIngredient;


        private List<IngredientRecipe> _showedIngredientRecipes;


        public void AddIngredientRecipe(IngredientKey ingredientKey, int collectedCount, int neededCount)
        {
            if(_showedIngredientRecipes == null)
                _showedIngredientRecipes = new List<IngredientRecipe>();
            
            IngredientRecipe newIngredientRecipe = null;

            switch (ingredientKey)
            {
                case IngredientKey.Cheese:
                    newIngredientRecipe = _cheeseIngredient;
                    break;
                case IngredientKey.Lettuce:
                    newIngredientRecipe = _lettuceIngredient;
                    break;
                case IngredientKey.Meat:
                    newIngredientRecipe = _meatIngredient;
                    break;
                case IngredientKey.Tomato:
                    newIngredientRecipe = _tomatoIngredient;
                    break;
            }

            newIngredientRecipe = Instantiate(newIngredientRecipe, transform);
            newIngredientRecipe.SetRatioText(collectedCount, neededCount);
            
            _showedIngredientRecipes.Add(newIngredientRecipe);
        }


        public void DestroyOldIngredientsRecipe()
        {
            if (_showedIngredientRecipes == null)
                return;
            
            int countIngredients = _showedIngredientRecipes.Count;

            for (int i = 0; i < countIngredients; i++)
            {
                Destroy(_showedIngredientRecipes[i].gameObject);
            }
            
            _showedIngredientRecipes.Clear();
        }


        public float CalculatePercentageMade()
        {
            if (_showedIngredientRecipes == null)
                return 0;
            
            float numberOfTypesOfIngredientsNeeded = _showedIngredientRecipes.Count;
            float numberOfIngredientsFullyHarvested = 0f;

            foreach (var ingredientRecipe in _showedIngredientRecipes)
            {
                if (ingredientRecipe.CollectedCount >= ingredientRecipe.NeededCount)
                    numberOfIngredientsFullyHarvested++;
            }

            float percentageMade = numberOfIngredientsFullyHarvested / numberOfTypesOfIngredientsNeeded;

            return percentageMade;
        }
    }   
}
