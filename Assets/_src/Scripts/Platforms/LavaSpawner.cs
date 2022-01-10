using BurgerHeroes.Obstacles;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Platforms
{
    public class LavaSpawner : MonoBehaviour
    {
        [SerializeField, AssetsOnly, Required]
        private Lava _lavaPrefab;


        [SerializeField, ChildGameObjectsOnly, Required]
        private Transform _lavaHolder;


        [SerializeField, ChildGameObjectsOnly, Required]
        private List<Transform> _lavaSpawnPoints;


        [SerializeField]
        private float _spawnChance;


        public void SpawnLava()
        {
            for (int i = 0; i < _lavaSpawnPoints.Count; i++)
            {
                if (Random.Range(0f, 1f) <= _spawnChance)
                {
                    Lava newLava = Instantiate(_lavaPrefab);
                    newLava.transform.SetParent(_lavaHolder);
                    newLava.transform.position = _lavaSpawnPoints[i].position;
                }
            }
        }
    }
}
