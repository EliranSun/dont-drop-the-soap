using UnityEngine;

public class CursorManager : MonoBehaviour {
    [SerializeField] private Texture2D defaultTexture;
    public Texture2D CurrentTexture { get; set; }
    public bool IsActionCursor { get; set; }
    public bool IsScrubbingCursor { get; set; }
    public bool IsSoapCursor { get; set; }
    public static CursorManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void GrabItem(
        Texture2D texture,
        Vector2 hotSpot,
        CursorMode cursorMode,
        SpriteRenderer sprite,
        GameObject indicator,
        bool physicalPick
    ) {
        Cursor.SetCursor(texture, hotSpot, cursorMode);
        Instance.CurrentTexture = texture;

        print($"texture name {texture.name}");

        // TODO: Enums
        if (texture.name == "soap-cursor")
            Instance.IsSoapCursor = true;
        if (texture.name == "sponge-cursor")
            Instance.IsScrubbingCursor = true;

        Instance.IsActionCursor = physicalPick;
        sprite.enabled = false;

        if (physicalPick)
            EventManager.Instance.Publish(GameEvents.PickedItem);

        if (indicator)
            indicator.SetActive(false);
    }

    public void DropItem(SpriteRenderer sprite, GameObject indicator) {
        Cursor.SetCursor(defaultTexture, Vector2.zero, CursorMode.Auto);

        // TODO: Enums
        if (Instance.CurrentTexture.name == "soap-cursor") {
            Instance.IsSoapCursor = false;
            EventManager.Instance.Publish(GameEvents.SoapDropped);
        }

        if (Instance.CurrentTexture.name == "sponge-cursor")
            Instance.IsScrubbingCursor = false;

        Instance.IsActionCursor = false;
        Instance.CurrentTexture = defaultTexture;
        EventManager.Instance.Publish(GameEvents.DroppedItem);

        sprite.enabled = true;

        if (indicator)
            indicator.SetActive(true);
    }

    public bool CanDropItem(Texture2D texture) {
        return texture && Instance.CurrentTexture == texture;
    }
}