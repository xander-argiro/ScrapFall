using UnityEngine;
using UnityEngine.InputSystem;

public class InteractZone : MonoBehaviour
{
    public enum InteractionType
    {
        Item,
        Door,
        Enemy,
        End
    }
    public InteractionType interactionType = InteractionType.Item;
    public enum Items
    {
        NOT_AN_ITEM,
        Key,
        Potion,
        Sword
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
        GameManager gameManager = FindAnyObjectByType<GameManager>();

        if (isPlayerInZone && Keyboard.current.eKey.wasPressedThisFrame)
        {
            switch (interactionType)
            {
                
                case InteractionType.Item:
                    ItemPickup(gameManager);
                    break;
                
                case InteractionType.Door:

                    if (gameManager.hasKey)
                    {
                        Debug.Log("Door opened!");
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("You need a key to open this door.");
                    }

                    break;

                case InteractionType.Enemy:
                    if (gameManager.hasSword)
                    {
                        Debug.Log("Enemy defeated!");
                        Destroy(gameObject);
                    }
                    else
                    {
                        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
                        player.Life_Current -= 25; // Reduce player's health
                    }

                    break;

                case InteractionType.End:
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

    private void ItemPickup(GameManager gameManager)
    {
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

            case Items.Sword:
                gameManager.swordText.text = "Sword: Yes";
                gameManager.hasSword = true;

                audioManager.pickupItem.Play();

                Debug.Log("Picked up a sword!");
                break;

            default:
                Debug.Log("Picked up an unknown item.");
                break;
        }

        Destroy(gameObject);
    }
}
