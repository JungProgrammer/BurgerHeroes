using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField, Required] 
        private Animator _animator;


        private const string _runTrigger = "RunTrigger";
        private const string _winTrigger = "WinTrigger";
        private const string _idleTrigger = "IdleTrigger";


        public void SetIdleState()
        {
            _animator.SetTrigger(_idleTrigger);
        }


        public void SetRunState()
        {
            _animator.SetTrigger(_runTrigger);
        }


        public void SetWinState()
        {
            _animator.SetTrigger(_winTrigger);
        }
    }   
}
