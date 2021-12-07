using System.Collections.Generic;
using BurgerHeroes.Event;
using BurgerHeroes.Food;
using BurgerHeroes.Levels;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace BurgerHeroes.UI
{
    public class PostGameplayWindow : MonoBehaviour
    {
        [SerializeField, SceneObjectsOnly, Required]
        private CoinsView _collectedInLevelCoinsView;
        
        
        [SerializeField, ChildGameObjectsOnly, Required]
        private RectTransform _levelPanel;


        [SerializeField, ChildGameObjectsOnly, Required]
        private TextMeshProUGUI _levelNumberText;


        [SerializeField, ChildGameObjectsOnly, Required]
        private StarsPanel _starsPanel;


        [SerializeField, ChildGameObjectsOnly, Required]
        private RectTransform _gotCoinsPanel;
        
        
        [SerializeField, ChildGameObjectsOnly, Required]
        private TextMeshProUGUI _gotCoinsText;
        
        
        [SerializeField, ChildGameObjectsOnly, Required]
        private NeededRecipeIngredients _collectedRecipeIngredients;


        [SerializeField, ChildGameObjectsOnly, Required]
        private RectTransform _collectButton;


        [SerializeField, Required] 
        private Image _background;


        [SerializeField] 
        private float _timeToOpenAnimation;


        [SerializeField] 
        private float _timeToCloseAnimation;


        [SerializeField, AssetsOnly, Required] 
        private GameEvent _openNextLevel;


        private Vector3 _startLevelPanelPosition;

        private Vector3 _startStarsPanelPosition;

        private Vector3 _startGotCoinsPanelPosition;

        private Vector3 _startCollectedRecipeIngredientsPosition;

        private Vector3 _startCollectButtonPosition;

        private Color _startBackgroundColor;
        
        
        private Vector3 _closedLevelPanelPosition;

        private Vector3 _closedStarsPanelPosition;
        
        private Vector3 _closedGotCoinsPanelPosition;
        
        private Vector3 _closedCollectedRecipeIngredientsPosition;
        
        private Vector3 _closedCollectButtonPosition;

        private Color _closedColor;
        
        
        private void OnEnable()
        {
            SetCollectedIngredientsInPostFinishWindow();
            OpenWindow();
        }


        private void Awake()
        {
            GetStartAndEndValues();
        }


        private void GetStartAndEndValues()
        {
            float startHeightDelta = Screen.height;
            
            _startLevelPanelPosition = _levelPanel.position;

            _startStarsPanelPosition = _starsPanel.transform.position;

            _startGotCoinsPanelPosition = _gotCoinsPanel.position;

            _startCollectedRecipeIngredientsPosition = _collectedRecipeIngredients.transform.position;

            _startCollectButtonPosition = _collectButton.position;

            _startBackgroundColor = _background.color;
            
            _closedLevelPanelPosition = new Vector3(
                _startLevelPanelPosition.x,
                _startLevelPanelPosition.y + startHeightDelta,
                _startLevelPanelPosition.z);

            _closedStarsPanelPosition = new Vector3(
                _startStarsPanelPosition.x,
                _startStarsPanelPosition.y + startHeightDelta,
                _startStarsPanelPosition.z);

            _closedGotCoinsPanelPosition = new Vector3(
                _startGotCoinsPanelPosition.x,
                _startGotCoinsPanelPosition.y + startHeightDelta,
                _startGotCoinsPanelPosition.z);
            
            _closedCollectedRecipeIngredientsPosition = new Vector3(
                _startCollectedRecipeIngredientsPosition.x,
                _startCollectedRecipeIngredientsPosition.y - startHeightDelta,
                _startCollectedRecipeIngredientsPosition.z);
            
            _closedCollectButtonPosition = new Vector3(
                _startCollectButtonPosition.x,
                _startCollectButtonPosition.y - startHeightDelta,
                _startCollectButtonPosition.z);
            
            _closedColor = new Color(
                _startBackgroundColor.r,
                _startBackgroundColor.g,
                _startBackgroundColor.b,
                0);
        }
        
        
        private void SetCollectedIngredientsInPostFinishWindow()
        {
            Dictionary<IngredientKey, int> collectedIngredients = HamburgerManager.Instance.GetCollectedIngredients();
            Dictionary<IngredientKey, int> neededLevelIngredients = LevelsManager.Instance.GetLevelIngredients();

            _collectedRecipeIngredients.DestroyOldIngredientsRecipe();
            foreach (var neededIngredient in neededLevelIngredients)
            {
                _collectedRecipeIngredients.AddIngredientRecipe(neededIngredient.Key, collectedIngredients[neededIngredient.Key],
                    neededIngredient.Value);
            }
            
            _starsPanel.UnloadStars();

            float percentageMade = _collectedRecipeIngredients.CalculatePercentageMade();
            _starsPanel.LoadStars(percentageMade);
        }


        public void OpenWindow()
        {
            _levelPanel.position = _closedLevelPanelPosition;
            _starsPanel.transform.position = _closedStarsPanelPosition;
            _gotCoinsPanel.position = _closedGotCoinsPanelPosition;
            _collectedRecipeIngredients.transform.position = _closedCollectedRecipeIngredientsPosition;
            _collectButton.position = _closedCollectButtonPosition;
            _background.color = _closedColor;

            Sequence openWindowSequence = DOTween.Sequence();
            openWindowSequence.Join(_levelPanel.DOMove(_startLevelPanelPosition, _timeToOpenAnimation));
            openWindowSequence.Join(_starsPanel.transform.DOMove(_startStarsPanelPosition, _timeToOpenAnimation));
            openWindowSequence.Join(_gotCoinsPanel.DOMove(_startGotCoinsPanelPosition, _timeToOpenAnimation));
            openWindowSequence.Join(
                _collectedRecipeIngredients.transform.DOMove(_startCollectedRecipeIngredientsPosition,
                    _timeToOpenAnimation));
            openWindowSequence.Join(_collectButton.DOMove(_startCollectButtonPosition, _timeToOpenAnimation));
            openWindowSequence.Join(_background.DOColor(_startBackgroundColor, _timeToOpenAnimation));

            
            int currentLevelNumber = PlayerPrefs.GetInt("LevelNumber", 1);
            _levelNumberText.text = "Recipe " + currentLevelNumber;
            PlayerPrefs.SetInt("LevelNumber", currentLevelNumber + 1);
            
            _gotCoinsText.text = "+" + _collectedInLevelCoinsView.CollectedCoinsCount.ToString();
        }
        
        
        public void CloseWindow()
        {
            Color superimposedColor = new Color(
                _startBackgroundColor.r,
                _startBackgroundColor.g,
                _startBackgroundColor.b,
                1);
            
            Sequence closeWindowSequence = DOTween.Sequence();
            closeWindowSequence.Join(_levelPanel.DOMove(_closedLevelPanelPosition, _timeToCloseAnimation));
            closeWindowSequence.Join(_starsPanel.transform.DOMove(_closedStarsPanelPosition, _timeToCloseAnimation));
            closeWindowSequence.Join(_gotCoinsPanel.DOMove(_closedGotCoinsPanelPosition, _timeToCloseAnimation));
            closeWindowSequence.Join(
                _collectedRecipeIngredients.transform.DOMove(_closedCollectedRecipeIngredientsPosition,
                    _timeToCloseAnimation));
            closeWindowSequence.Join(_collectButton.DOMove(_closedCollectButtonPosition, _timeToCloseAnimation));
            closeWindowSequence.Append(_background.DOColor(superimposedColor, _timeToCloseAnimation / 2f));
            closeWindowSequence.OnComplete(() =>
            {
                _openNextLevel.Raise();
            });
        }
    }   
}
