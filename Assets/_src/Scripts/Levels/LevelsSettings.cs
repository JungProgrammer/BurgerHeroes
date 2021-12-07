using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Levels
{
    [CreateAssetMenu(menuName = "LevelsSettings"), Serializable]
    public class LevelsSettings : ScriptableObject
    {
        [SerializeField, AssetsOnly, Required] 
        private List<Level> _levels = new List<Level>();


        public int CountOfLevels => _levels.Count;


        public Level GetLevelOnId(int id) => _levels.Count > id ? _levels[id] : null;
    }   
}
