using UnityEngine;

public class NPCPolice : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public Transform[] waypoints; // Массив контрольных точек
    public float speed = 3.0f; // Скорость движения
    public float rotationSpeed = 5.0f; // Скорость поворота
    public float detectionRange = 5.0f; // Дистанция, с которой NPC будет реагировать на игрока
    public float speedDetection = 6f; // Скорость NPC при преследовании
    public float catchRange = 1.0f; // Дистанция, при которой NPC догоняет игрока
    public float obstacleDetectionDistance = 1.0f; // Дистанция для обнаружения препятствий

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
        // Проверка на догонку игрока

    }

    void ChasePlayer()
    {
        // Направление к игроку
        Vector3 direction = (player.position - transform.position).normalized;

        // Поворот NPC в направлении игрока
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Проверка на препятствия
        if (!Physics.Raycast(transform.position, transform.forward, obstacleDetectionDistance))
        {
            // Движение NPC вперед
            transform.position += transform.forward * speedDetection * Time.deltaTime;

            // Включение анимации бега
            if (animator != null)
            {
                animator.SetBool("isWalking", true); // Замените "isWalking" на нужный параметр анимации.
            }
        }
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

        // Проверка на препятствия
        if (!Physics.Raycast(transform.position, transform.forward, obstacleDetectionDistance))
        {
            // Движение NPC вперед
            transform.position += transform.forward * speed * Time.deltaTime;

            // Включение анимации патрулирования
            if (animator != null)
            {
                animator.SetBool("isWalking", true); // Замените "isWalking" на нужный параметр анимации.
            }
        }
    }

    void CatchPlayer()
    {
        // Действие при догонке игрока
        Debug.Log("Игрок пойман!");

        // Проверьте, существует ли Inventory.Instance и метод RemoveInventory
        if (Inventory.Instance != null)
        {
            Inventory.Instance.RemoveInventory();
        }
        else
        {
            Debug.LogWarning("Inventory.Instance is null or RemoveInventory method is missing!");
        }

        // Остановка NPC после догонки (если нужно)
        isPlayerInRange = false;
        Patrol();
    }
}