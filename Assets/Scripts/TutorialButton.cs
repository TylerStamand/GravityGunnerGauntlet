using UnityEngine;
using UnityEngine.Events;

public class TutorialButton : MonoBehaviour {

    [SerializeField] Sprite pressedSprite;

    public UnityEvent Pressed;

    new SpriteRenderer renderer;

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        PlayerUnit playerUnit = collision.gameObject.GetComponent<PlayerUnit>();
        if(playerUnit != null) {
            renderer.sprite = pressedSprite;
            Pressed?.Invoke();
        }
        
    }
}
