using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Analytics;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.UI
{
    public class StartInfo : MonoBehaviour
    {
        [SerializeField, SceneObjectsOnly, Required]
        private MainMenu _mainMenu;
        
        
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                AmplitudeManager.Instance.SendLevelStartEvent();
                AmplitudeManager.Instance.SetLastLevel();
                
                _mainMenu.StartGameplay();
            }
        }
    }   
}
