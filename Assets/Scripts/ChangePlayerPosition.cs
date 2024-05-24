using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ChangePlayerPosition : MonoBehaviour {
    [SerializeField] private PositionName positionName;
    [SerializeField] private Transform playerTransform;

    private void Start() {
        EventManager.Instance.Publish(GameEvents.OutOfShower);
    }

    private void OnMouseDown() {
        if (CursorManager.Instance.IsActionCursor)
            return;

        playerTransform.position = transform.position;
        SetGameState();
        var positions = GameObject.FindGameObjectsWithTag("PlayerPosition");

        foreach (var position in positions) {
            var isSelf = position.name == name;
            position.GetComponent<Collider2D>().enabled = !isSelf;
            position.GetComponent<SpriteRenderer>().enabled = !isSelf;

            if (isSelf)
                DisableChildren(position);
            else
                EnableChildren(position);
        }
    }

    private void EnableChildren(GameObject parent) {
        foreach (Transform child in parent.transform) child.gameObject.SetActive(true);
    }

    // Method to disable all children
    private void DisableChildren(GameObject parent) {
        foreach (Transform child in parent.transform) child.gameObject.SetActive(false);
    }

    private void SetGameState() {
        GameState.IsPlayerInShower = positionName == PositionName.InShower;
    }
}