using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField, Required]
        private Transform _exitPoint;


        [SerializeField, ChildGameObjectsOnly]
        private Curved _curved;


        [SerializeField, ChildGameObjectsOnly]
        private IngredientsSpawner _ingredientsSpawner;


        [SerializeField, ChildGameObjectsOnly]
        private LavaSpawner _lavaSpawner;


        [SerializeField, ChildGameObjectsOnly]
        private ObstacleSpawner _obstacleSpawner;


        public Transform ExitPoint => _exitPoint;


        public Curved Curved => _curved;


        public void SpawnIngredients()
        {
            if (_ingredientsSpawner == null)
                return;


            _ingredientsSpawner.SpawnIngredients();
        }


        public void SpawnLava()
        {
            if (_lavaSpawner == null)
                return;


            _lavaSpawner.SpawnLava();
        }


        public void SpawnObstacles()
        {
            if (_obstacleSpawner == null)
                return;


            _obstacleSpawner.SpawnObstacles();
        }
    }
}
