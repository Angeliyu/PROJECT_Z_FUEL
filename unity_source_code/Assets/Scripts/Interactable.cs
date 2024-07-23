using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool isPickupItem = false; // To differentiate between a pickup item and a regular interactable
    public bool requiresItemPickup = false;
    public Image interactionImage;

    private int collectedGasolin = 0;

    private void Start()
    {
        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(false);  // image set to hidden initially
        }
    }

    public void Interact(PlayerMovement player)
    {
        if (isPickupItem)
        { 
            // Item pickup logic
            if (player.hasPickedUpItem)
            {
                Debug.Log("Can't pick up multiple item " + gameObject.name);
            }
            else
            {
                player.hasPickedUpItem = true;
                Destroy(gameObject);
            }
        }
        else if (requiresItemPickup)
        {
            if (player.hasPickedUpItem)
            {
                collectedGasolin += 1;
                if(collectedGasolin >= 3)
                {
                    SceneManager.LoadSceneAsync(3);
                }
                player.hasPickedUpItem = false;
            }
        }
        else
        {
            Debug.Log("Interacted with " + gameObject.name);
        }
    }
}
