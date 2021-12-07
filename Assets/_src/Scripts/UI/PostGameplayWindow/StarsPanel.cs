using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.UI
{
    public class StarsPanel : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly, Required]
        private Star _firstStar;
        
        
        [SerializeField, ChildGameObjectsOnly, Required]
        private Star _secondStar;
        
        
        [SerializeField, ChildGameObjectsOnly, Required]
        private Star _thirdStar;


        [SerializeField] 
        private float _percentageMadeForFirstStar;
        
        
        [SerializeField] 
        private float _percentageMadeForSecondStar;
        
        
        [SerializeField] 
        private float _percentageMadeForThirdStar;


        public void LoadStars(float percentageMade)
        {
            if (percentageMade >= _percentageMadeForFirstStar)
                _firstStar.SetCompleteStar();
            
            if (percentageMade >= _percentageMadeForSecondStar)
                _secondStar.SetCompleteStar();
            
            if (percentageMade >= _percentageMadeForThirdStar)
                _thirdStar.SetCompleteStar();
        }


        public void UnloadStars()
        {
            _firstStar.SetUncompleteStar();
            _secondStar.SetUncompleteStar();
            _thirdStar.SetUncompleteStar();
        }
    }   
}
