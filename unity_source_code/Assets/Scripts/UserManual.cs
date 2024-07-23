using UnityEngine;
using UnityEngine.UI;

public class UserManual : MonoBehaviour
{
    public Image uiImage; // Reference to the UI Image
    public Transform player; // Reference to the player object
    private Vector3 lastPlayerPosition; // To store the last player position
    private bool isImageHidden = false; // Flag to check if the image is already hidden

    void Start()
    {
        // Initialize the last player position
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // Check if the player has moved
        if (!isImageHidden && player.position != lastPlayerPosition)
        {
            // Hide the UI image and set the flag
            uiImage.enabled = false;
            isImageHidden = true;
        }

        // Update the last player position
        lastPlayerPosition = player.position;
    }
}
