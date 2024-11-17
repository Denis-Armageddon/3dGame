using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform player; // ������ �� ������ ������
    public Transform[] waypoints; // ������ ����������� �����
    public float speed = 3.0f; // �������� ��������
    public float rotationSpeed = 5.0f; // �������� ��������
    public float detectionRange = 5.0f; // ���������, � ������� NPC ����� ����������� �� ������

    private int currentWaypointIndex = 0;
    private bool isPlayerInRange = false;



    private void Start()
    {
       
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                StopNPC();
            }

            RotateTowardsPlayer();
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                ContinueNPCMovement();
            }

            Patrol();
        }
    }

    void StopNPC()
    {
        // ���������� �������� NPC. ���� � ���� ���� Rigidbody, ����� ���������� ��� ��������.
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }

        // ��������� ��������, ���� ��� �������.
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", false); // �������� "isWalking" �� ������ �������� ��������.
        }
    }

    void ContinueNPCMovement()
    {
        // ���������� �������� NPC. �������� �������� ��� ��������.
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", true); // �������� "isWalking" �� ������ �������� ��������.
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void Patrol()
    {
        if (waypoints.Length == 0)
            return;

        // ����������� � ������� ����������� �����
        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
        direction.y = 0; // ���������, ��� NPC �� ������ ������

        // ���� NPC ������ ����������� �����
        if (direction.magnitude < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            direction = waypoints[currentWaypointIndex].position - transform.position;
        }

        // ������� NPC � ����������� ����������� �����
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // �������� NPC ������
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}