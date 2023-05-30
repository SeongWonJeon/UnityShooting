using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCameraController : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] float mouseSensitivity;

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;


    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    // ī�޶�� ������Ʈ�� ���������ʱ� ����� LateUpdate�� �����ϱ�
    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;   // ���� ������
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;   // �� �Ʒ� ** ���̳ʽ��� ����� �ݴ�� �ؾ��Ѵ�.
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);  // Ŭ������� ���а������ִµ�, �� �Ʒ��� �ּ� 80�� ������ �� �� �ֵ���

        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
