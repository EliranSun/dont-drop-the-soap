using UnityEngine;

namespace Hands.Scripts {
    public class DroppedTheSoap : MonoBehaviour {
        private bool _isSoapDropped;

        private void Update() {
            if (transform.position.y < -10 && !_isSoapDropped) {
                SoapManager.IsSoapDropped = true;
                // local variable is important because we want to trigger this once
                _isSoapDropped = true;
            }
        }
    }
}