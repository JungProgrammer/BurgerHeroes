using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] 
        private Transform _leftLimiter;
        
        
        [SerializeField] 
        private Transform _rightLimiter;


        public Vector3 LeftLimiterPosition => _leftLimiter.position;
        public Vector3 RightLimiterPosition => _rightLimiter.position;
    }   
}
