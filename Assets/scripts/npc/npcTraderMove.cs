using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public Transform[] waypoints; // Массив контрольных точек
    public float speed = 3.0f; // Скорость движения
    public float rotationSpeed = 5.0f; // Скорость поворота
    public float detectionRange = 5.0f; // Дистанция, с которой NPC будет реагировать на игрока

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
        // Остановить движение NPC. Если у него есть Rigidbody, можно остановить его скорость.
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }

        // Остановка анимаций, если они имеются.
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", false); // Замените "isWalking" на нужный параметр анимации.
        }
    }

    void ContinueNPCMovement()
    {
        // Продолжить движение NPC. Включить анимации или движение.
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", true); // Замените "isWalking" на нужный параметр анимации.
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

        // Направление к текущей контрольной точке
        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
        direction.y = 0; // Убедитесь, что NPC не меняет высоту

        // Если NPC достиг контрольной точки
        if (direction.magnitude < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            direction = waypoints[currentWaypointIndex].position - transform.position;
        }

        // Поворот NPC в направлении контрольной точки
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Движение NPC вперед
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}