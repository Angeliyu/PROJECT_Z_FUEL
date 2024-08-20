using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool isPickupItem = false; // To differentiate between a pickup item and a regular interactable
    public bool requiresItemPickup = false;
    public AudioSource pickUpSoundEffect;
    public AudioSource fillGasolineSoundEffect;

    public void Interact(PlayerMovement player)
    {
        if (isPickupItem)
        {
            // Item pickup logic
            if (player.hasPickedUpItem)
            {
                Debug.Log("Can't pick up multiple items " + gameObject.name);
            }
            else
            {
                PlaySoundAndDestroy(player);
            }
        }
        else if (requiresItemPickup)
        {
            if (player.hasPickedUpItem)
            {
                fillGasolineSoundEffect.Play();
                ProgressTracking.Instance.CollectGasoline();
                player.hasPickedUpItem = false;
            }
        }
        else
        {
            Debug.Log("Interacted with " + gameObject.name);
        }
    }

    private void PlaySoundAndDestroy(PlayerMovement player)
    {
        if (pickUpSoundEffect != null)
        {
            // Detach the AudioSource from the GameObject
            pickUpSoundEffect.transform.SetParent(null);
            pickUpSoundEffect.Play();
            Destroy(pickUpSoundEffect.gameObject, pickUpSoundEffect.clip.length);
        }

        player.hasPickedUpItem = true;
        Destroy(gameObject);
    }
}
