using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float maxStamina = 100f;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;
    public bool hasPickedUpItem = false;
    public AudioSource footStepSoundEffect;
    public Rigidbody2D rb;
    public Animator animator;
    private float currentStamina = 100f;
    private Vector2 movement;
    private Interactable currentInteractable;


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

        // Regenerate stamina if not sprinting
        if (!Input.GetKey(KeyCode.LeftShift) || movement.sqrMagnitude == 0)
        {
            RegenerateStamina();
        }

        playerUI.Instance.SetStamina(currentStamina);
        playerFootstepSoundEffect();
    }

    void FixedUpdate()
    {
        // Determine the current speed
        float currentSpeed = moveSpeed;

        // Check if the sprint button is held down
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && movement.sqrMagnitude > 0)
        {
            currentSpeed = sprintSpeed;
            DrainStamina();
        }

        // Apply the movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

    void DrainStamina()
    {
        currentStamina -= staminaDrainRate * Time.deltaTime;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    void RegenerateStamina()
    {
        currentStamina += staminaRegenRate * Time.deltaTime;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger of an interactable object
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.GetComponent<Interactable>();

            // show the pick up hint
            HintManage.Instance.ShowOrHideHint(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the trigger of an interactable object
        if (other.CompareTag("Interactable"))
        {
            // hide the pick up hint
            HintManage.Instance.ShowOrHideHint(false);

            if (currentInteractable != null && other.GetComponent<Interactable>() == currentInteractable)
            {
                currentInteractable = null;
            }
        }
    }

    void playerFootstepSoundEffect()
    {
        if (movement != Vector2.zero)
        {
            if (!footStepSoundEffect.isPlaying)
            {
                footStepSoundEffect.Play();
            }
        }
        else
        {
            if (footStepSoundEffect.isPlaying)
            {
                footStepSoundEffect.Pause();
            }
        }
    }
}
