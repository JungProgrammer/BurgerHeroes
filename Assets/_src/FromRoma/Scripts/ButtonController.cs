using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BurgerHeroes.Gate
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private GateController gateToTrigger;
        [SerializeField] private ColorsCollection colorsCollection;
        [SerializeField] private ButtonColors buttonColor;
        [SerializeField] private MeshRenderer buttonActivePartMesh;
        [SerializeField] private Transform activePartTransform;

        private bool _isUntouched = true;

        private void Awake() {
            buttonActivePartMesh.material = colorsCollection.GetButtonMaterial(buttonColor);
        }

        private void OnTriggerEnter(Collider other) {
            if (_isUntouched) {
                _isUntouched = false;
                ButtonPress();
            }
        }

        private void ButtonPress() {
            activePartTransform.DOMove(
                activePartTransform.position - new Vector3(0, 0.2f, 0), 0.1f);
            gateToTrigger.OnButtonPress(buttonColor);
        }
    }
}
