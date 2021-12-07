using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Event;
using BurgerHeroes.Food;
using BurgerHeroes.Player;
using BurgerHeroes.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


namespace BurgerHeroes.Levels
{
    public class LevelsManager : Singleton<LevelsManager>
    {
        [SerializeField, Required] 
        private PlayerMovement _playerMovement;


        [SerializeField, Required] 
        private Transform _playerSpawnPoint;


        [SerializeField, SceneObjectsOnly, Required]
        private Transform _levelHolder;


        [SerializeField, AssetsOnly, Required] 
        private LevelsSettings _levelsSettings;


        [SerializeField, AssetsOnly, Required] 
        private GameEvent _levelLoadedEvent;
        
        
        [Title("For tests")]
        [SerializeField, AssetsOnly] 
        private Level _neededLevel;


        private Level _loadedLevel;
        private LevelRecipe _loadedLevelRecipe;


        private int _countOfCompletedLevels;
        

        private void Start()
        {
            OpenNextLevel();
        }


        private void SetLevelSettings()
        {
            _playerMovement.gameObject.transform.position = _playerSpawnPoint.position;
            _playerMovement.SetLimiters(_loadedLevel.LeftLimiterPosition, _loadedLevel.RightLimiterPosition);

            _loadedLevelRecipe = _loadedLevel.transform.GetComponent<LevelRecipe>();

            _levelLoadedEvent.Raise();
        }


        public void OpenNextLevel()
        {
            if (_loadedLevel != null)
                Destroy(_loadedLevel.gameObject);
            
            // Для теста в редакторе
            if (_neededLevel != null)
            {
                _loadedLevel = Instantiate(_neededLevel, _levelHolder);
                SetLevelSettings();
                return;
            }
            
            
            _countOfCompletedLevels = PlayerPrefs.GetInt("LevelNumber", 1) - 1;

            int indexOfNextLoadLevel;

            if (_countOfCompletedLevels < _levelsSettings.CountOfLevels)
                indexOfNextLoadLevel = _countOfCompletedLevels;
            else
                indexOfNextLoadLevel = Random.Range(0, _levelsSettings.CountOfLevels);

            _loadedLevel = Instantiate(_levelsSettings.GetLevelOnId(indexOfNextLoadLevel), _levelHolder);
            
            SetLevelSettings();
        }


        public Dictionary<IngredientKey, int> GetLevelIngredients()
        {
            if (_loadedLevelRecipe == null)
                return new Dictionary<IngredientKey, int>();
            
            return _loadedLevelRecipe.GetNeedLevelIngredients();
        }
    }   
}
