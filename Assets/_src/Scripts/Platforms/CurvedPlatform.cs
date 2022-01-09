using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Platforms
{
    public class CurvedPlatform : Platform
    {
        [SerializeField]
        private float _rotateDegree;


        private bool _reached;


        public float RotateDegree => _rotateDegree;


        public bool Reached => _reached;


        public void SetReached()
        {
            _reached = true;
        }
    }
}
