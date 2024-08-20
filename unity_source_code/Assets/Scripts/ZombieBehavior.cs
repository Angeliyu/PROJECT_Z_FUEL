using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chasingSpeed = 4f;
    public Transform[] roamPoints;
    public float chasePersistenceTime = 1f;
    public float fieldOfViewAngle = 180f;
    public float viewDistance = 10f;
    public delegate void StateChanged();
    public static event StateChanged OnStateChanged;
    public bool isChasing = false;
    private float lastPlayerSightingTime;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;
    private int currentRoamPoint = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNextRoamPoint();
    }

    void Update()
    {
        if (hasLineOfSight())
        {
            isChasing = true;
            lastPlayerSightingTime = Time.time;
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                if (Time.time - lastPlayerSightingTime < chasePersistenceTime)
                {
                    ChasePlayer();
                }
                else
                {
                    isChasing = false;
                    SetNextRoamPoint();
                }
            }
            Roam();
        }

        OnStateChanged?.Invoke();
        UpdateAnimations();
    }

    private bool hasLineOfSight()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < viewDistance)
        {
            Vector2 forward = rb.velocity.normalized;
            float angle = Vector2.Angle(forward, directionToPlayer);

            if (angle < fieldOfViewAngle / 2)
            {
                RaycastHit2D ray = Physics2D.Raycast(transform.position, directionToPlayer);

                if (ray.collider != null)
                {
                    // Debug.Log("Raycast hit: " + ray.collider.name); // Debugging line to see what the ray hits
                    if (ray.collider.CompareTag("Player"))
                    {
                        Debug.DrawRay(transform.position, directionToPlayer, Color.green);
                        return true;
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, directionToPlayer, Color.red);
                    }
                }
                else
                {
                    // Debug.Log("ray collider is null");
                }
            }
            else
            {
                // Debug.Log("angle out of sight");
            }
        }
        return false;
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chasingSpeed;
    }

    void Roam()
    {
        if (Vector2.Distance(transform.position, roamPoints[currentRoamPoint].position) < 0.5f)
        {
            SetNextRoamPoint();
        }

        Vector2 direction = (roamPoints[currentRoamPoint].position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void SetNextRoamPoint()
    {
        currentRoamPoint = (currentRoamPoint + 1) % roamPoints.Length;
    }

    void UpdateAnimations()
    {
        Vector2 direction = rb.velocity;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }
}