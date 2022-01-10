using BurgerHeroes.Platforms;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField, AssetsOnly, Required]
    private Platform[] _platforms;


    [SerializeField, AssetsOnly, Required]
    private Platform _finishPlatform;


    [SerializeField]
    private int _countForSpawnPlatform;


    [SerializeField, Required]
    private Transform _startSpawnPoint;


    [SerializeField, Required]
    private Transform _platformsHolder;


    private Platform _previousPlatform;


    private int _randomIndex;


    private void Awake()
    {
        GenerateLevel();
    }


    private void GenerateLevel()
    {
        for (int i = 0; i < _countForSpawnPlatform; i++)
        {
            GeneratePlatform();
        }


        GenerateFinish();
    }


    private void GeneratePlatform()
    {
        _randomIndex = Random.Range(0, _platforms.Length);
        while (CheckRepeatability()) { }

        Platform newPlatform = Instantiate(_platforms[_randomIndex], _platformsHolder);


        if (_previousPlatform != null)
        {
            newPlatform.transform.position = _previousPlatform.ExitPoint.position;
            newPlatform.transform.rotation = _previousPlatform.transform.rotation;

            if (_previousPlatform.Curved != null)
                newPlatform.transform.eulerAngles = new Vector3(newPlatform.transform.eulerAngles.x, newPlatform.transform.eulerAngles.y + _previousPlatform.Curved.RotateDegree, newPlatform.transform.eulerAngles.z);
        }

        _previousPlatform = newPlatform;


        newPlatform.SpawnIngredients();
        newPlatform.SpawnLava();
        newPlatform.SpawnObstacles();
    }


    private void GenerateFinish()
    {
        Platform finishPlatform = Instantiate(_finishPlatform, _platformsHolder);

        finishPlatform.transform.position = _previousPlatform.ExitPoint.position;
        finishPlatform.transform.rotation = _previousPlatform.transform.rotation;

        if (_previousPlatform.Curved != null)
            finishPlatform.transform.eulerAngles = new Vector3(finishPlatform.transform.eulerAngles.x, finishPlatform.transform.eulerAngles.y + _previousPlatform.Curved.RotateDegree + 180, finishPlatform.transform.eulerAngles.z);
    }


    private bool CheckRepeatability()
    {
        if (_previousPlatform == null)
            return false;


        if (_platforms[_randomIndex].Curved != null && _previousPlatform.Curved != null)
        {
            _randomIndex = Random.Range(0, _platforms.Length);
            return true;
        }
        else
            return false;
    }
}
