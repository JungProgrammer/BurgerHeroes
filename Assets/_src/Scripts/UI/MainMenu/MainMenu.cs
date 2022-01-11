using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Analytics;
using BurgerHeroes.Event;
using BurgerHeroes.Levels;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


namespace BurgerHeroes.UI
{
    public class MainMenu : MonoBehaviour {
        [SerializeField, AssetsOnly, Required]
        private GameEvent _startGameplayEvent;


        [SerializeField, ChildGameObjectsOnly, Required]
        private TextMeshProUGUI _levelText;


        [SerializeField, ChildGameObjectsOnly, Required]
        private NeededRecipeIngredients _neededMenuRecipeIngredients;

        [SerializeField, AssetsOnly, Required]
        private GameEvent _openShop;

        private void OnEnable() {
            LoadLevelText();

            AmplitudeManager.Instance.SendMenuLevelEvent();
        }


        public void LoadMenuRecipeIngredients() {
            _neededMenuRecipeIngredients.DestroyOldIngredientsRecipe();
            foreach (var levelIngredient in LevelsManager.Instance.GetLevelIngredients()) {
                _neededMenuRecipeIngredients.AddIngredientRecipe(levelIngredient.Key, 0, levelIngredient.Value);
            }
        }


        private void Awake() {
            LoadLevelText();
        }

        private void Start() {
            if (!PlayerPrefs.HasKey("LevelNumber")) {
                _startGameplayEvent.Raise();
            }
        }


        private void LoadLevelText() {
            _levelText.text = "Recipe " + PlayerPrefs.GetInt("LevelNumber", 1);
        }


        public void StartGameplay() {
            _startGameplayEvent.Raise();
        }

        public void OpenShop() {
            _openShop.Raise();
        }
    }   
}
