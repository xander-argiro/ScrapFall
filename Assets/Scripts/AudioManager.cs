using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource victorySound;
    public AudioSource gameOverSound;
    public AudioSource pickupItem;
    public AudioSource pickupKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        victorySound.playOnAwake = false;
        gameOverSound.playOnAwake = false;
        pickupItem.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
