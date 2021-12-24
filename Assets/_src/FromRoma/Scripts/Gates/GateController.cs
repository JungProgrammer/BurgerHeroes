using UnityEngine;
using Sirenix.OdinInspector;
using BurgerHeroes.Event;
using DG.Tweening;

namespace BurgerHeroes.Gate
{
    public class GateController : SerializedMonoBehaviour
    {
        [SerializeField] private ColorsCollection colorsCollection;
        [SerializeField] private ButtonColors colorForGateActivation;
        [SerializeField] private MeshRenderer gateMesh;
        [SerializeField] private GameObject gateQuad;
        [SerializeField] private Transform leftPillar;
        [SerializeField] private Transform rightPillar;
        [SerializeField] private GameEvent gateCollidedEvent;

        private bool _isUntouched = true;

        private void Awake() {
            gateMesh.material = colorsCollection.GetGateMaterial(colorForGateActivation);
        }

        private void OnTriggerEnter(Collider other) {
            if (_isUntouched) {
                _isUntouched = false;
                gateCollidedEvent.Raise();
            }
        }

        public void OnButtonPress(ButtonColors triggeredButtonColor) {
            if (triggeredButtonColor == colorForGateActivation) {
                OpenGate();
            }
        }

        private void OpenGate() {
            _isUntouched = false;
            Destroy(gateQuad);
            leftPillar.DOMoveY(-10, 1);
            rightPillar.DOMoveY(-10, 1);
        }
    }
}