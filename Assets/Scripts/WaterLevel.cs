using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterLevel : MonoBehaviour {
    [SerializeField] private float minWaterLevel;
    [SerializeField] private float pumpingLevelChange = 0.05f;
    [SerializeField] private float waterLevelChange = 0.01f;

    [FormerlySerializedAs("waterThrottle")] [SerializeField]
    private float waterVerticalTransition;

    private void Update() {
        if (waterVerticalTransition == 0)
            return;

        transform.Translate(new Vector2(0, waterVerticalTransition * Time.deltaTime));
    }

    public void OnNotify(GameEventData eventData) {
        switch (eventData.name) {
            case GameEvents.Pumping:
                if (transform.localPosition.y <= minWaterLevel)
                    break;

                StartCoroutine(TemporaryWaterChange(-pumpingLevelChange));
                break;

            case GameEvents.FaucetOpening:
                waterVerticalTransition += waterLevelChange;
                break;

            case GameEvents.FaucetClosing:
                if (waterVerticalTransition <= 0)
                    return;

                waterVerticalTransition -= waterLevelChange;
                break;
        }
    }

    private IEnumerator TemporaryWaterChange(float levelChange) {
        waterVerticalTransition += levelChange;
        yield return new WaitForSeconds(1);
        waterVerticalTransition -= levelChange;
    }
}