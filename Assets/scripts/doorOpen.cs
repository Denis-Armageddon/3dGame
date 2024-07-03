using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public Camera cameraPlayer;
    public float interactionDistance = 3f; // ������������ ��������� ��� ��������������
    public float rotationSpeed = 1f; // �������� �������� �����

    private bool isDoorOpen = false; // ��������� �����
    private bool isRotating = false; // �������� �� ���������� ��������

    private Quaternion targetRotation; // �������� ��������� �����
    private Quaternion initialRotation; // ��������� ��������� �����
    private float rotationProgress = 0f; // �������� ��������

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
                door.transform.rotation = targetRotation; // ���������, ��� ����� ����� � �������� ���������
                isRotating = false;
            }
        }
    }

    private void ToggleDoor()
    {
        initialRotation = door.transform.rotation;
        if (isDoorOpen)
        {
            // �������� �����
            targetRotation = Quaternion.Euler(-90, 0, 90);
        }
        else
        {
            // �������� �����
            targetRotation = Quaternion.Euler(-90, 0, 0);
        }

        rotationProgress = 0f;
        isRotating = true;
        isDoorOpen = !isDoorOpen;
    }
}