using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Food
{
    public class FoodCollisionChecker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out Ingredient ingredient))
            {
                ingredient.Collect();
                HamburgerManager.Instance.AddCollectedIngredientToHamburger(ingredient);
            }
        }
    }   
}
