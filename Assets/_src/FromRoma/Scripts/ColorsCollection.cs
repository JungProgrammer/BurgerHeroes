using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BurgerHeroes.Gate
{
    [CreateAssetMenu(menuName = "ColorCollection")]
    public class ColorsCollection : SerializedScriptableObject
    {
        [SerializeField, AssetsOnly]
        private Dictionary<ButtonColors, Material> gateMaterials;

        [SerializeField, AssetsOnly]
        private Dictionary<ButtonColors, Material> buttonMaterials;

        public Material GetGateMaterial(ButtonColors buttonColor) {
            return gateMaterials[buttonColor];
        }

        public Material GetButtonMaterial(ButtonColors buttonColor) {
            return buttonMaterials[buttonColor];
        }
    }
}