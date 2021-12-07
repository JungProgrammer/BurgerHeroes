using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Coins;
using BurgerHeroes.Obstacles;
using UnityEngine;
using Random = UnityEngine.Random;


namespace BurgerHeroes.Food
{
    public class HamburgerPart : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;


        [SerializeField] 
        private HamburgerPartSettings _hamburgerPartSettings;


        [SerializeField] 
        private Collider _hamburgerPartCollider;
        

        private HamburgerPart _previousHamburgerPart;
        public HamburgerPart PreviousHamburgerPart => _previousHamburgerPart;
        
        private HamburgerPart _nextHamburgerPart;
        public HamburgerPart NextHamburgerPart => _nextHamburgerPart;


        public IngredientKey _ingredientKey => _hamburgerPartSettings.IngredientKey;
        
        public float Height => _hamburgerPartSettings.Height;


        public void SetPreviousHamburgerPart(HamburgerPart hamburgerPart)
        {
            _previousHamburgerPart = hamburgerPart;
        }
        
        public void SetNextHamburgerPart(HamburgerPart hamburgerPart)
        {
            _nextHamburgerPart = hamburgerPart;
        }

        public void ThrowPartFromHamburger()
        {
            transform.SetParent(null);
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            Vector3 randomForceVector = new Vector3(Random.Range(-1f, 1f), 0, 0);
            _rigidbody.AddForce(randomForceVector * _hamburgerPartSettings.SpeedDropPart, ForceMode.Impulse);
            Destroy(gameObject, _hamburgerPartSettings.TimeToDisappearAfterThrow);
            Destroy(this);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FlyingObstacle obstacle))
            {
                obstacle.ReduceStrength();
                HamburgerManager.Instance.DestroyHamburgerPart(this);
            }
            else if (other.TryGetComponent(out Coin coin))
            {
                coin.Collect();
            }
        }


        public void TriggerOff()
        {
            _hamburgerPartCollider.enabled = false;
        }
    }
}
