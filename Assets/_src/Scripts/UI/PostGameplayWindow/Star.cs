using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace BurgerHeroes.UI
{
    public class Star : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly, Required]
        private Image _completedStar;


        [SerializeField, ChildGameObjectsOnly, Required]
        private Image _uncompletedStar;


        public void SetCompleteStar()
        {
            _completedStar.enabled = true;
            _uncompletedStar.enabled = false;
        }


        public void SetUncompleteStar()
        {
            _completedStar.enabled = false;
            _uncompletedStar.enabled = true;
        }
    }   
}
