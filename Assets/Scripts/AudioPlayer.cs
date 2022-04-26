using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
    AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        
    }
    void Update() {
        if(!audioSource.isPlaying) {
            Destroy(gameObject);
        }
    }
}
