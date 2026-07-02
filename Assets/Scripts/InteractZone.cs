using UnityEngine;
using UnityEngine.InputSystem;

public class InteractZone : MonoBehaviour
{
    public enum InteractionType
    {
        Item,
        Door
    }
    public InteractionType interactionType = InteractionType.Item;
    public enum Items
    {
        NOT_AN_ITEM,
        Key,
        Potion
    }
    public Items itemType = Items.NOT_AN_ITEM;

    private bool isPlayerInZone;

    private void Start()
    {
        isPlayerInZone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }

    private void Update()
    {
        if (isPlayerInZone && Keyboard.current.eKey.wasPressedThisFrame)
        {

            switch (interactionType)
            {
                case InteractionType.Item:
                    ItemPickup();
                    break;

                case InteractionType.Door:
                    GameManager gameManager = FindAnyObjectByType<GameManager>();

                    if (gameManager.hasKey)
                    {
                        gameManager.Victory();
                    }
                    else
                    {
                        Debug.Log("You need a key to open this door.");
                    }

                    break;
            }
        }
    }

    private void ItemPickup()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
            return;
        }

        AudioManager audioManager = FindAnyObjectByType<AudioManager>();

        switch (itemType)
        {
            case Items.Key:
                gameManager.hasKey = true;
                gameManager.keyText.text = "Key: Yes";

                audioManager.pickupKey.Play();

                Debug.Log("Picked up a key!");

                break;

            case Items.Potion:
                gameManager.potionText.text = "Potion: Yes";

                audioManager.pickupItem.Play();

                Debug.Log("Picked up a potion!");
                
                break;

            default:
                Debug.Log("Picked up an unknown item.");
                break;
        }

        Destroy(gameObject);
    }
}
