using UnityEngine;

public class NPCPolice : MonoBehaviour
{
    public Transform player; // ������ �� ������ ������
    public Transform[] waypoints; // ������ ����������� �����
    public float speed = 3.0f; // �������� ��������
    public float rotationSpeed = 5.0f; // �������� ��������
    public float detectionRange = 5.0f; // ���������, � ������� NPC ����� ����������� �� ������
    public float speedDetection = 6f; // �������� NPC ��� �������������
    public float catchRange = 1.0f; // ���������, ��� ������� NPC �������� ������
    public float obstacleDetectionDistance = 1.0f; // ��������� ��� ����������� �����������

    private int currentWaypointIndex = 0;
    private bool isPlayerInRange = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);


        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
            ChasePlayer();

        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
            }

            Patrol();
        }
        if (distanceToPlayer <= catchRange)
        {
            CatchPlayer();
        }
        // �������� �� ������� ������

    }

    void ChasePlayer()
    {
        // ����������� � ������
        Vector3 direction = (player.position - transform.position).normalized;

        // ������� NPC � ����������� ������
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // �������� �� �����������
        if (!Physics.Raycast(transform.position, transform.forward, obstacleDetectionDistance))
        {
            // �������� NPC ������
            transform.position += transform.forward * speedDetection * Time.deltaTime;

            // ��������� �������� ����
            if (animator != null)
            {
                animator.SetBool("isWalking", true); // �������� "isWalking" �� ������ �������� ��������.
            }
        }
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

        // �������� �� �����������
        if (!Physics.Raycast(transform.position, transform.forward, obstacleDetectionDistance))
        {
            // �������� NPC ������
            transform.position += transform.forward * speed * Time.deltaTime;

            // ��������� �������� ��������������
            if (animator != null)
            {
                animator.SetBool("isWalking", true); // �������� "isWalking" �� ������ �������� ��������.
            }
        }
    }

    void CatchPlayer()
    {
        // �������� ��� ������� ������
        Debug.Log("����� ������!");

        // ���������, ���������� �� Inventory.Instance � ����� RemoveInventory
        if (Inventory.Instance != null)
        {
            Inventory.Instance.RemoveInventory();
        }
        else
        {
            Debug.LogWarning("Inventory.Instance is null or RemoveInventory method is missing!");
        }

        // ��������� NPC ����� ������� (���� �����)
        isPlayerInRange = false;
        Patrol();
    }
}