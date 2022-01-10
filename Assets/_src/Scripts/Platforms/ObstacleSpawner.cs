using BurgerHeroes.Obstacles;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Platforms
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField, AssetsOnly, Required]
        private FlyingObstacle _knifePrefab;


        [SerializeField, ChildGameObjectsOnly, Required]
        private Transform _knifesHolder;


        [SerializeField, ChildGameObjectsOnly, Required]
        private List<Transform> _knifeSpawnPoints;


        [SerializeField]
        private float _spawnChance;


        public void SpawnObstacles()
        {
            for (int i = 0; i < _knifeSpawnPoints.Count; i++)
            {
                if (Random.Range(0f, 1f) <= _spawnChance)
                {
                    FlyingObstacle newKnife = Instantiate(_knifePrefab);
                    newKnife.transform.SetParent(_knifesHolder);
                    newKnife.transform.position = _knifeSpawnPoints[i].position;
                }
            }
        }
    }
}
