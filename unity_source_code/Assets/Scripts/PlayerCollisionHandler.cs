using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has the "NPC" tag
        if (collision.gameObject.CompareTag("Zombie"))
        {
            // Load the Game Over scene
            SceneManager.LoadSceneAsync(2);
        }
    }
}
