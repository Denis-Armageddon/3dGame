using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform[] waypoints; // ������ ����������� �����
    public float speed = 3.0f; // �������� ��������
    public float rotationSpeed = 5.0f; // �������� ��������
    private int currentWaypointIndex = 0;

    private void Update()
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