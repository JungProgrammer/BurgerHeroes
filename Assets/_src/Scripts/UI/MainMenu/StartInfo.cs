using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Analytics;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;


namespace BurgerHeroes.UI
{
    public class StartInfo : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField, SceneObjectsOnly, Required]
        private MainMenu _mainMenu;

        public void OnPointerDown(PointerEventData eventData)
        {
            AmplitudeManager.Instance.SendLevelStartEvent();
            AmplitudeManager.Instance.SetLastLevel();

            _mainMenu.StartGameplay();
        }
    }   
}
