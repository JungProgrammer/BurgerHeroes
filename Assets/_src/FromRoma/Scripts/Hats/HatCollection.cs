using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BurgerHeroes.Hats
{
    [CreateAssetMenu(menuName = "Hat Collection")]
    public class HatCollection: SerializedScriptableObject
    {
        [SerializeField, AssetsOnly]
        private Dictionary<HatVariants, GameObject> hatPrefabs;

        public GameObject GetHatPrefab(HatVariants hatVariant) {
            return hatPrefabs[hatVariant];
        }
    }
}