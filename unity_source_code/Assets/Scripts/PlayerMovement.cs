using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;

    private Interactable currentInteractable;
    public bool hasPickedUpItem = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetBool("HasPickedUpItem", hasPickedUpItem);

        // Check for interaction input
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }
    }

    void FixedUpdate()
    {
        // Determine the current speed
        float currentSpeed = moveSpeed;

        // Check if the sprint button is held down (Left Shift by default)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }

        // Apply the movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger of an interactable object
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.GetComponent<Interactable>();

            // show the pick up  hint to player
            currentInteractable.interactionImage.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the trigger of an interactable object
        if (other.CompareTag("Interactable"))
        {
            // hide the pick up button hint
            currentInteractable.interactionImage.gameObject.SetActive(false);

            if (currentInteractable != null && other.GetComponent<Interactable>() == currentInteractable)
            {
                currentInteractable = null;
            }
        }
    }
}
