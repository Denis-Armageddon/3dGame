using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform[] waypoints; // Массив контрольных точек
    public float speed = 3.0f; // Скорость движения
    public float rotationSpeed = 5.0f; // Скорость поворота
    private int currentWaypointIndex = 0;

    private void Update()
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