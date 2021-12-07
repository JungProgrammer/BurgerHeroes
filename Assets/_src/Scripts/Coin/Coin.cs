using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Event;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField, AssetsOnly, Required] 
        private GameEvent _getCoinEvent;


        [SerializeField, ChildGameObjectsOnly, Required]
        private ParticleSystem _collectParticles;


        [SerializeField, ChildGameObjectsOnly, Required]
        private Collider _collider;


        [SerializeField, Required] 
        private MeshRenderer _coinMeshRenderer;


        [SerializeField] 
        private float _timeToDestroyAfterCollect;
        


        public void Collect()
        {
            _collider.enabled = false;
            _getCoinEvent.Raise();
            _collectParticles.Play();
            _coinMeshRenderer.enabled = false;

            Destroy(gameObject, _timeToDestroyAfterCollect);
        }
    }   
}
