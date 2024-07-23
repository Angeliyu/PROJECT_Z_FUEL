using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public Transform[] roamPoints;
    public float detectionRange = 10f;
    public float chasingSpeed = 5f;
    public float roamingSpeed = 2f;
    public float stoppingDistance = 0.5f;
    public Animator animator;

    private int currentRoamPointIndex = 0;
    private Transform player;
    private Rigidbody2D rb;
    private bool isChasing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GoToNextRoamPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < detectionRange)
        {
            StartChasing();
        }
        else if (isChasing)
        {
            StopChasing();
        }

        if (isChasing)
        {
            MoveTowards(player.position, chasingSpeed);
        }
        else
        {
            if (Vector3.Distance(transform.position, roamPoints[currentRoamPointIndex].position) <= stoppingDistance)
            {
                GoToNextRoamPoint();
            }
            else
            {
                MoveTowards(roamPoints[currentRoamPointIndex].position, roamingSpeed);
            }
        }
    }

    void StartChasing()
    {
        isChasing = true;
    }

    void StopChasing()
    {
        isChasing = false;
        GoToNextRoamPoint();
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        rb.velocity = direction * speed;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    void GoToNextRoamPoint()
    {
        if (roamPoints.Length == 0)
            return;

        rb.velocity = Vector2.zero; // Stop movement
        currentRoamPointIndex = (currentRoamPointIndex + 1) % roamPoints.Length;
    }
}
