using System;
using UnityEngine;

public class WaterLevel : MonoBehaviour {
    [SerializeField] private float waterThrottle;

    private void Update() {
        if (waterThrottle == 0)
            return;

        transform.Translate(new Vector2(0, waterThrottle * Time.deltaTime));
    }

    public void OnNotify(GameEvents eventName) {
        switch (eventName) {
            case GameEvents.FaucetOpening:
                waterThrottle += 0.05f;
                break;

            case GameEvents.FaucetClosing:
                if (waterThrottle <= 0)
                    return;

                waterThrottle -= 0.05f;
                break;

            case GameEvents.None:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(eventName), eventName, null);
        }
    }
}