using BurgerHeroes.Food;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ingredients Data")]
public class IngredientsData : SerializedScriptableObject
{
    [SerializeField]
    private Dictionary<IngredientKey, Ingredient> _ingredientPrefabs = new Dictionary<IngredientKey, Ingredient>();


    public Dictionary<IngredientKey, Ingredient> IngredientPrefabs => _ingredientPrefabs;


    public Ingredient GetIngredientOnIngredientKey(IngredientKey ingredientKey)
    {
        return _ingredientPrefabs[ingredientKey];
    }
}
