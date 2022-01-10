using BurgerHeroes.Food;
using BurgerHeroes.Levels;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Platforms
{
    public class IngredientsSpawner : MonoBehaviour
    {
        [SerializeField, AssetsOnly, Required]
        private IngredientsData _ingredientsData;


        [SerializeField, ChildGameObjectsOnly, Required]
        private List<Transform> _ingredientSpawnPoints;


        [SerializeField]
        private float _spawnChance;


        private void SpawnIngredient(Vector3 spawnPoint)
        {
            if (Random.Range(0f, 1f) > _spawnChance)
                return;


            Ingredient newIngredient = Instantiate(_ingredientsData.GetIngredientOnIngredientKey(LevelRecipe.Instance.GetRandomIngredientKey()));
            newIngredient.transform.SetParent(LevelRecipe.Instance.IngredientsHolder);
            newIngredient.transform.position = spawnPoint;
        }


        public void SpawnIngredients()
        {
            for (int i = 0; i < _ingredientSpawnPoints.Count; i++)
            {
                SpawnIngredient(_ingredientSpawnPoints[i].position);
            }
        }
    }
}
