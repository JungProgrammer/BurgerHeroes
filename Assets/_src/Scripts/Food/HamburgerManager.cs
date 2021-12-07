using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Coins;
using BurgerHeroes.Event;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


namespace BurgerHeroes.Food
{
    public class HamburgerManager : Singleton<HamburgerManager>
    {
        [SerializeField, Required] 
        private Transform _spawnTransformParent;
        
        
        [SerializeField, Required] 
        private Transform _spawnPoint;


        [SerializeField, Required] 
        private Transform _breadTopTransform;


        [SerializeField, Required] 
        private Transform _breadBottomTransform;


        [SerializeField, Required] 
        private MeshRenderer _plateMeshRenderer;


        [SerializeField] 
        private float _randomXAmplitudeForCollectedParts;


        [SerializeField] 
        private float _collectAnimationSpeed;
        
        
        [SerializeField, Required]
        private Dictionary<IngredientKey, HamburgerPart> _hamburgerPartsPrefabs = new Dictionary<IngredientKey, HamburgerPart>();


        [SerializeField, AssetsOnly, Required] 
        private List<IngredientKey> _initialHamburgerIngredientKeys;


        [Title("PostFinish")] 
        [SerializeField, AssetsOnly, Required]
        private Coin _coinPrefab;


        [SerializeField, AssetsOnly, Required] 
        private GameEvent _hamburgerTurnedToCoins;


        [Title("Settings for initial hamburger")]
        [Button]
        private void AddCheeseToHamburger()
        {
            _initialHamburgerIngredientKeys.Add(IngredientKey.Cheese);
        }
        
        
        [Button]
        private void AddMeatToHamburger()
        {
            _initialHamburgerIngredientKeys.Add(IngredientKey.Meat);
        }
        
        
        [Button]
        private void AddTomatoToHamburger()
        {
            _initialHamburgerIngredientKeys.Add(IngredientKey.Tomato);
        }
        
        
        [Button]
        private void AddLettuceToHamburger()
        {
            _initialHamburgerIngredientKeys.Add(IngredientKey.Lettuce);
        }


        private Dictionary<IngredientKey, int> _collectedIngredients;
        

        private const float _zDelta = 0.4f;
        
        private List<HamburgerPart> _collectedHamburgerParts;
        private HamburgerPart _previousHamburgerPart;

        private float _currentHumburgerHeight;
        
        
        // hamburger settings
        private Vector3 _startSpawnPointLocalPosition;
        private Vector3 _startBreadTopLocalPosition;


        private void Awake()
        {
            SetHamburgerSettings();
            InitializeStarterHamburger();
        }


        private void SetHamburgerSettings()
        {
            _startSpawnPointLocalPosition = _spawnPoint.localPosition;
            _startBreadTopLocalPosition = _breadTopTransform.localPosition;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(_startSpawnPointLocalPosition, .3f);
        }


        private void RaiseSpawnPoint(float heightDelta)
        {
            _spawnPoint.localPosition = new Vector3(
                _spawnPoint.localPosition.x,
                _spawnPoint.localPosition.y,
                _spawnPoint.localPosition.z - heightDelta);
        }


        private void RaiseBreadTopTransform(float heightDelta)
        {
            _breadTopTransform.localPosition = new Vector3(
                _breadTopTransform.localPosition.x,
                _breadTopTransform.localPosition.y,
                _breadTopTransform.localPosition.z - heightDelta);
        }


        private void MoveCollectedHamburgerPartInStraightLines(Ingredient ingredient, Vector3 targetPoint)
        {
            Transform hamburgerPartTransform = ingredient.transform;

            float randomXDirection = Random.Range(-_randomXAmplitudeForCollectedParts, _randomXAmplitudeForCollectedParts);
            
            
            Vector3 lateralPosition = new Vector3(
                hamburgerPartTransform.position.x + randomXDirection,
                hamburgerPartTransform.position.y,
                hamburgerPartTransform.position.z + _zDelta);

            Vector3 topLateralPosition = new Vector3(
                lateralPosition.x,
                targetPoint.y,
                lateralPosition.z + _zDelta);

            Vector3 centerEndPosition = new Vector3(
                targetPoint.x,
                targetPoint.y,
                lateralPosition.z + _zDelta);

            // var sequence = DOTween.Sequence();
            // sequence.Append(hamburgerPartTransform.DOMove(lateralPosition, 1 / _collectAnimationSpeed, false));
            // sequence.Append(hamburgerPartTransform.DOMove(topLateralPosition, 1 / _collectAnimationSpeed, false));
            // sequence.Append(hamburgerPartTransform.DOMove(centerEndPosition, 1 / _collectAnimationSpeed, false));
            // sequence.Join(hamburgerPart.transform.DOScale(Vector3.zero, 1 / _collectAnimationSpeed));
            // sequence.OnComplete(() => { Destroy(hamburgerPart.gameObject); });
            
            var sequence = DOTween.Sequence();
            sequence.Append(hamburgerPartTransform.DOMove(lateralPosition, 1 / _collectAnimationSpeed, false));
            sequence.Append(hamburgerPartTransform.DOMove(topLateralPosition, 1 / _collectAnimationSpeed, false));
            sequence.OnComplete(() =>
            {
                StartCoroutine(MoveHamburgerPartToLastPoint(hamburgerPartTransform));
            });
        }


        private void MoveCollectedHamburgerPartInDiagonalLines(Ingredient ingredient, Vector3 targetPoint)
        {
            Transform hamburgerPartTransform = ingredient.transform;
            
            float randomXDirection = Random.Range(-_randomXAmplitudeForCollectedParts, _randomXAmplitudeForCollectedParts);

            Vector3 lateralCenterHamburgerPoint = new Vector3(
                hamburgerPartTransform.position.x + randomXDirection,
                hamburgerPartTransform.position.y + _currentHumburgerHeight / 3,
                hamburgerPartTransform.position.z + _zDelta);

            var sequence = DOTween.Sequence();
            sequence.Append(hamburgerPartTransform.DOMove(lateralCenterHamburgerPoint, 1 / _collectAnimationSpeed,
                false));
            sequence.Join(hamburgerPartTransform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1 / _collectAnimationSpeed));
            
            sequence.OnComplete(() =>
            {
                StartCoroutine(MoveHamburgerPartToLastPoint(hamburgerPartTransform));
            });
        }


        private void AddHamburgerPartToList(HamburgerPart newHamburgerPart)
        {
            _collectedHamburgerParts.Add(newHamburgerPart);
            newHamburgerPart.SetPreviousHamburgerPart(_previousHamburgerPart);
            if (_previousHamburgerPart != null)
            {
                _previousHamburgerPart.SetNextHamburgerPart(newHamburgerPart);
            }

            _previousHamburgerPart = newHamburgerPart;
        }


        private void AddHamburgerPartToHamburgerInStartGame(IngredientKey ingredientKey)
        {
            HamburgerPart newHamburgerPart = Instantiate(_hamburgerPartsPrefabs[ingredientKey],
                _spawnPoint.position, _spawnPoint.rotation);
            newHamburgerPart.transform.SetParent(_spawnTransformParent);
            newHamburgerPart.transform.Rotate(0, 0, Random.Range(-180, 180));

            _currentHumburgerHeight += newHamburgerPart.Height;

            AddHamburgerPartToList(newHamburgerPart);
            
            float heightDelta = newHamburgerPart.Height / 2;
            RaiseSpawnPoint(heightDelta);
            RaiseBreadTopTransform(heightDelta);
        }


        private void CalculateCollectedIngredients()
        {
            _collectedIngredients = new Dictionary<IngredientKey, int>();
            _collectedIngredients.Add(IngredientKey.Cheese, 0);
            _collectedIngredients.Add(IngredientKey.Lettuce, 0);
            _collectedIngredients.Add(IngredientKey.Meat, 0);
            _collectedIngredients.Add(IngredientKey.Tomato, 0);

            for (int i = 0; i < _collectedHamburgerParts.Count; i++)
            {
                _collectedIngredients[_collectedHamburgerParts[i]._ingredientKey]++;
            }
        }


        public Dictionary<IngredientKey, int> GetCollectedIngredients()
        {
            return _collectedIngredients;
        }
        
        
        public void InitializeStarterHamburger()
        {
            _breadTopTransform.gameObject.SetActive(true);
            _breadBottomTransform.gameObject.SetActive(true);
            _plateMeshRenderer.enabled = true;

            _spawnPoint.localPosition = _startSpawnPointLocalPosition;
            _breadTopTransform.localPosition = _startBreadTopLocalPosition;
            
            _previousHamburgerPart = null;
            _currentHumburgerHeight = 0;
            _collectedHamburgerParts = new List<HamburgerPart>();
            
            foreach (IngredientKey ingredientKey in _initialHamburgerIngredientKeys)
            {
                AddHamburgerPartToHamburgerInStartGame(ingredientKey);
            }
        }
        

        public void AddCollectedIngredientToHamburger(Ingredient ingredient)
        {
            HamburgerPart newHamburgerPart = Instantiate(_hamburgerPartsPrefabs[ingredient.IngredientKey],
                _spawnPoint.position, _spawnPoint.rotation);
            newHamburgerPart.transform.SetParent(_spawnTransformParent);
            newHamburgerPart.transform.Rotate(0, 0, Random.Range(-180, 180));

            _currentHumburgerHeight += newHamburgerPart.Height;
            
            ingredient.transform.SetParent(_spawnTransformParent);
            MoveCollectedHamburgerPartInDiagonalLines(ingredient, _spawnPoint.position);
            
            AddHamburgerPartToList(newHamburgerPart);

            float heightDelta = newHamburgerPart.Height / 2;
            RaiseSpawnPoint(heightDelta);
            RaiseBreadTopTransform(heightDelta);
        }


        public void DestroyHamburgerPart(HamburgerPart hamburgerPart)
        {
            HamburgerPart foundHamburgerPart = _collectedHamburgerParts.Find(part => part == hamburgerPart);
            if(foundHamburgerPart == _collectedHamburgerParts[_collectedHamburgerParts.Count - 1])
                return;


            if (foundHamburgerPart.PreviousHamburgerPart != null)
                foundHamburgerPart.PreviousHamburgerPart.SetNextHamburgerPart(foundHamburgerPart.NextHamburgerPart);
            if (foundHamburgerPart.NextHamburgerPart != null)
                foundHamburgerPart.NextHamburgerPart.SetPreviousHamburgerPart(foundHamburgerPart.PreviousHamburgerPart);

            _currentHumburgerHeight -= foundHamburgerPart.Height;

            float heightDelta = foundHamburgerPart.Height / 2;
            
            HamburgerPart currentHamburgerPart = foundHamburgerPart.NextHamburgerPart;
            while (currentHamburgerPart != null)
            {
                Transform currentPartTransform = currentHamburgerPart.transform;
                
                currentPartTransform.localPosition = new Vector3(
                    currentPartTransform.localPosition.x,
                    currentPartTransform.localPosition.y,
                    currentPartTransform.localPosition.z + heightDelta);

                currentHamburgerPart = currentHamburgerPart.NextHamburgerPart;
            }
            
            RaiseSpawnPoint(-heightDelta);
            RaiseBreadTopTransform(-heightDelta);
            
            foundHamburgerPart.ThrowPartFromHamburger();

            _collectedHamburgerParts.Remove(foundHamburgerPart);
        }
        
        
        public void TakeLavaDamage()
        {
            if (_collectedHamburgerParts.Count > 0)
            {
                HamburgerPart randomHamburgerPart = _collectedHamburgerParts[Random.Range(0, _collectedHamburgerParts.Count-2)];
            
                DestroyHamburgerPart(randomHamburgerPart);   
            }
        }


        public void TurningHamburgerIntoCoins()
        {
            CalculateCollectedIngredients();
            
            foreach (HamburgerPart hamburgerPart in _collectedHamburgerParts)
            {
                hamburgerPart.TriggerOff();
            }
            
            StartCoroutine(TurnHamburgerToCoinsCoroutine());
        }


        private void MoveCoinOnPostFinishToCollect(Coin coin)
        {
            float randomXOffset =
                Random.Range(-_randomXAmplitudeForCollectedParts, _randomXAmplitudeForCollectedParts);
            
            Transform newCoinTransform = coin.transform;
            newCoinTransform.DOMove(new Vector3(
                newCoinTransform.position.x + randomXOffset,
                newCoinTransform.position.y,
                newCoinTransform.position.z), 1.5f, false).OnComplete(() =>
            {
                coin.Collect();
            });
        }


        private IEnumerator TurnHamburgerToCoinsCoroutine()
        {
            List<Coin> _spawnedCoinsFromHamburger = new List<Coin>();
            
            foreach (HamburgerPart hamburgerPart in _collectedHamburgerParts)
            {
                Coin newCoin = Instantiate(_coinPrefab, hamburgerPart.transform.position, Quaternion.identity);
                newCoin.transform.SetParent(_spawnTransformParent);
                _spawnedCoinsFromHamburger.Add(newCoin);
                
                Destroy(hamburgerPart.gameObject);

                yield return new WaitForSeconds(.03f);
            }
            
            _collectedHamburgerParts.Clear();
            
            _breadTopTransform.gameObject.SetActive(false);
            _breadBottomTransform.gameObject.SetActive(false);

            foreach (Coin coin in _spawnedCoinsFromHamburger)
            {
                MoveCoinOnPostFinishToCollect(coin);
            }
            

            if (_plateMeshRenderer.enabled)
                _plateMeshRenderer.enabled = false;

            // Подождать 3 секунды, чтобы открыть меню оконачания
            yield return new WaitForSeconds(3);
            _hamburgerTurnedToCoins.Raise();

            yield return null;
        }


        private IEnumerator MoveHamburgerPartToLastPoint(Transform hamburgerPartTransform)
        {
            float deltaPosition = Vector3.Distance(hamburgerPartTransform.position, _spawnPoint.position);

            hamburgerPartTransform.DOScale(new Vector3(.4f, .4f, .4f), deltaPosition / (_collectAnimationSpeed * 10));
            while (Vector3.Distance(hamburgerPartTransform.position, _spawnPoint.position) >= .3f)
            {
                hamburgerPartTransform.position = Vector3.MoveTowards(hamburgerPartTransform.position,
                    _spawnPoint.position, Time.deltaTime * _collectAnimationSpeed * 2);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            Destroy(hamburgerPartTransform.gameObject);
            yield return null;
        }
    }   
}
