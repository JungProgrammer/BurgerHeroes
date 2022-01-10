using BurgerHeroes.Event;
using BurgerHeroes.Food;
using BurgerHeroes.Levels;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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


        [SerializeField, ChildGameObjectsOnly, Required]
        private RectTransform _multiplyButton;


        [SerializeField, ChildGameObjectsOnly, Required]
        private RectTransform _multiplierScale;


        [SerializeField, Required] 
        private Image _background;


        [SerializeField] 
        private float _timeToOpenAnimation;


        [SerializeField] 
        private float _timeToCloseAnimation;


        [SerializeField, AssetsOnly, Required] 
        private GameEvent _openNextLevel;

        [Title("Multiplier Arrow")]
        [SerializeField, ChildGameObjectsOnly, Required]
        private RectTransform _multiplierArrow;
        [SerializeField, ChildGameObjectsOnly, Required]
        private RectTransform _multiplierArrowRotationPoint;
        private bool _multiplierArrowRotates;
        [SerializeField, Required]
        private float _multiplierArrowRotationSpeed;
        [SerializeField, Required]
        private float _multiplierArrowLeftBorder;
        [SerializeField, Required]
        private float _multiplierArrowRightBorder;
        [SerializeField, Required]
        private float _multiplierArrowMaxValueAngle;
        [SerializeField, Required]
        private float _multiplierArrowMaxValueDispersion;
        [SerializeField, Required]
        private float _multiplierArrowMaxMultiplier;
        [SerializeField, Required]
        private float _multiplierArrowMinMultiplier;
        [SerializeField, ChildGameObjectsOnly, Required]
        private TextMeshProUGUI _coinMultiplierCoefficientText;
        private float _coinMultiplierCoefficient;

        [SerializeField] private Color _maxMultiplierColor;
        [SerializeField] private Color _minMultiplierColor;


        private Vector3 _startLevelPanelPosition;

        private Vector3 _startStarsPanelPosition;

        private Vector3 _startGotCoinsPanelPosition;

        private Vector3 _startCollectedRecipeIngredientsPosition;

        private Vector3 _startCollectButtonPosition;

        private Vector3 _startMultiplyButtonPosition;

        private Vector3 _startMultiplierScalePosition;

        private Color _startBackgroundColor;
        
        
        private Vector3 _closedLevelPanelPosition;

        private Vector3 _closedStarsPanelPosition;
        
        private Vector3 _closedGotCoinsPanelPosition;
        
        private Vector3 _closedCollectedRecipeIngredientsPosition;
        
        private Vector3 _closedCollectButtonPosition;

        private Vector3 _closedMultiplyButtonPosition;

        private Vector3 _closedMultiplierScalePosition;

        private Color _closedColor;


        private Vector3 _endMultiplierScalePosition;
        
        
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

            _startMultiplyButtonPosition = _multiplyButton.position;

            _startMultiplierScalePosition = _multiplierScale.position;

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
                _startGotCoinsPanelPosition.y - startHeightDelta,
                _startGotCoinsPanelPosition.z);
            
            _closedCollectedRecipeIngredientsPosition = new Vector3(
                _startCollectedRecipeIngredientsPosition.x,
                _startCollectedRecipeIngredientsPosition.y - startHeightDelta,
                _startCollectedRecipeIngredientsPosition.z);
            
            _closedCollectButtonPosition = new Vector3(
                _startCollectButtonPosition.x,
                _startCollectButtonPosition.y - startHeightDelta,
                _startCollectButtonPosition.z);

            _closedMultiplyButtonPosition = new Vector3(
                _startCollectButtonPosition.x,
                _startCollectButtonPosition.y - startHeightDelta,
                _startCollectButtonPosition.z);

            _closedMultiplierScalePosition = new Vector3(
                _startCollectButtonPosition.x,
                _startCollectButtonPosition.y - startHeightDelta,
                _startCollectButtonPosition.z);

            _closedColor = new Color(
                _startBackgroundColor.r,
                _startBackgroundColor.g,
                _startBackgroundColor.b,
                0);

            _endMultiplierScalePosition = new Vector3(
                _startCollectButtonPosition.x,
                _startCollectButtonPosition.y + startHeightDelta,
                _startCollectButtonPosition.z);
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
            StartCoroutine(RotateArrow());

            _multiplyButton.gameObject.SetActive(true);
            _levelPanel.position = _closedLevelPanelPosition;
            _starsPanel.transform.position = _closedStarsPanelPosition;
            _gotCoinsPanel.position = _closedGotCoinsPanelPosition;
            _collectedRecipeIngredients.transform.position = _closedCollectedRecipeIngredientsPosition;
            _collectButton.position = _closedCollectButtonPosition;
            _multiplierScale.position = _closedMultiplierScalePosition;
            _multiplyButton.position = _closedMultiplyButtonPosition;
            _background.color = _closedColor;

            Sequence openWindowSequence = DOTween.Sequence();
            openWindowSequence.Join(_levelPanel.DOMove(_startLevelPanelPosition, _timeToOpenAnimation));
            openWindowSequence.Join(_starsPanel.transform.DOMove(_startStarsPanelPosition, _timeToOpenAnimation));
            openWindowSequence.Join(_multiplyButton.DOMove(_startMultiplyButtonPosition, _timeToOpenAnimation));
            openWindowSequence.Join(_multiplierScale.DOMove(_startMultiplierScalePosition, _timeToOpenAnimation));
            openWindowSequence.Join(_background.DOColor(_startBackgroundColor, _timeToOpenAnimation));
            
            int currentLevelNumber = PlayerPrefs.GetInt("LevelNumber", 1);
            _levelNumberText.text = "Recipe " + currentLevelNumber;
            PlayerPrefs.SetInt("LevelNumber", currentLevelNumber + 1);
        }
        
        public void MultiplierCollect() {
            _multiplierArrowRotates = false;
            _multiplyButton.gameObject.SetActive(false);

            StartCoroutine(OnMultiplierCollectSequenceLaunch());

            _collectedInLevelCoinsView.MultiplyCoins(_coinMultiplierCoefficient);
            _gotCoinsText.text = "+" + _collectedInLevelCoinsView.CollectedCoinsCount.ToString();
        }

        private IEnumerator OnMultiplierCollectSequenceLaunch() {
            yield return new WaitForSeconds(2f);
            Sequence openWindowSequence = DOTween.Sequence();
            openWindowSequence.Join(_gotCoinsPanel.DOMove(_startGotCoinsPanelPosition, _timeToOpenAnimation));
            openWindowSequence.Join(
                _collectedRecipeIngredients.transform.DOMove(_startCollectedRecipeIngredientsPosition,
                    _timeToOpenAnimation));
            openWindowSequence.Join(_collectButton.DOMove(_startCollectButtonPosition, _timeToOpenAnimation));
            openWindowSequence.Join(_multiplierScale.DOMove(_endMultiplierScalePosition, _timeToOpenAnimation));
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

        private IEnumerator RotateArrow() {
            _multiplierArrowRotates = true;
            Vector3 rotationAxis = new Vector3(0, 0, 1);
            while (_multiplierArrowRotates) {
                while (_multiplierArrow.rotation.eulerAngles.z < _multiplierArrowLeftBorder
                    && _multiplierArrowRotates) {
                    CalculateMultiplierCoefficient();
                    _multiplierArrow.RotateAround(
                        _multiplierArrowRotationPoint.position,
                        rotationAxis,
                        _multiplierArrowRotationSpeed*Time.deltaTime);
                    yield return null;
                }
                if (_multiplierArrowRotates) {
                    _multiplierArrow.rotation = Quaternion.Euler(0, 0, _multiplierArrowLeftBorder);
                    CalculateMultiplierCoefficient();
                    yield return null;
                }

                while (_multiplierArrow.rotation.eulerAngles.z > _multiplierArrowRightBorder
                    && _multiplierArrow.rotation.eulerAngles.z < 270
                    && _multiplierArrowRotates) {
                    CalculateMultiplierCoefficient();
                    _multiplierArrow.RotateAround(
                        _multiplierArrowRotationPoint.position,
                        -rotationAxis,
                        _multiplierArrowRotationSpeed * Time.deltaTime);
                    yield return null;
                }
                if (_multiplierArrowRotates) {
                    _multiplierArrow.rotation = Quaternion.Euler(0, 0, _multiplierArrowRightBorder);
                    CalculateMultiplierCoefficient();
                    yield return null;
                }
            }
        }

        private void CalculateMultiplierCoefficient() {
            float currentRotation = _multiplierArrow.rotation.eulerAngles.z;
            float leftMaxValueBorder = _multiplierArrowMaxValueAngle + _multiplierArrowMaxValueDispersion;
            float rightMaxValueBorder = _multiplierArrowMaxValueAngle - _multiplierArrowMaxValueDispersion;
            float valueOnTheInterval;

            if (currentRotation>=_multiplierArrowRightBorder 
                && currentRotation < rightMaxValueBorder) {
                Debug.Log("Uno");
                valueOnTheInterval = (currentRotation - _multiplierArrowRightBorder)
                    / (rightMaxValueBorder - _multiplierArrowRightBorder);
            }else if (currentRotation<=_multiplierArrowLeftBorder
                && currentRotation > leftMaxValueBorder) {
                Debug.Log("Dos");
                valueOnTheInterval = (currentRotation - leftMaxValueBorder)
                    / (_multiplierArrowLeftBorder - leftMaxValueBorder);
                valueOnTheInterval = 1 - valueOnTheInterval;
            } else {
                Debug.Log("Tres");
                valueOnTheInterval = 1;
            }
            _coinMultiplierCoefficient = valueOnTheInterval * _multiplierArrowMaxMultiplier;

            _coinMultiplierCoefficientText.text = string.Format("X{0:0.00}", _coinMultiplierCoefficient);
            _coinMultiplierCoefficientText.color = Color.Lerp(
                _minMultiplierColor,
                _maxMultiplierColor,
                valueOnTheInterval);
        }
    }   
}
