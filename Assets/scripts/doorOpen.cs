using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public Camera cameraPlayer;
    public float interactionDistance = 3f; // максимальная дистанция для взаимодействия
    public float rotationSpeed = 1f; // скорость вращения двери

    private bool isDoorOpen = false; // Состояние двери
    private bool isRotating = false; // Проверка на выполнение вращения

    private Quaternion targetRotation; // Конечное положение двери
    private Quaternion initialRotation; // Начальное положение двери
    private float rotationProgress = 0f; // Прогресс вращения

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.gameObject == door && !isRotating)
                {
                    ToggleDoor();
                }
            }
        }

        if (isRotating)
        {
            rotationProgress += Time.deltaTime * rotationSpeed;
            door.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, rotationProgress);

            if (rotationProgress >= 1f)
            {
                door.transform.rotation = targetRotation; // Убедиться, что дверь точно в конечном положении
                isRotating = false;
            }
        }
    }

    private void ToggleDoor()
    {
        initialRotation = door.transform.rotation;
        if (isDoorOpen)
        {
            // Закрытие двери
            targetRotation = Quaternion.Euler(-90, 0, 90);
        }
        else
        {
            // Открытие двери
            targetRotation = Quaternion.Euler(-90, 0, 0);
        }

        rotationProgress = 0f;
        isRotating = true;
        isDoorOpen = !isDoorOpen;
    }
}