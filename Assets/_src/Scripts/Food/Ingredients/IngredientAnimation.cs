using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace BurgerHeroes.Food
{
    public class IngredientAnimation : MonoBehaviour
    {
        [SerializeField] 
        private float _rotateSpeed;
        
        
        private void Start()
        {
            transform.DOMove(new Vector3(transform.position.x,
                    transform.position.y + .5f,
                    transform.position.z),
                2,
                false).SetLoops(-1, LoopType.Yoyo);
            transform.DOBlendableScaleBy(
                new Vector3(0.2f, 0.2f, 0.2f),
                UnityEngine.Random.Range(1.25f, 1.75f)).SetLoops(-1, LoopType.Yoyo);
        }


        private void Update()
        {
            transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
        }


        public void StopAnimation()
        {
            DOTween.Kill(transform);
        }
    }   
}
