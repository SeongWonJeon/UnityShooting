using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HWaIncingScene : MonoBehaviour
{
    [SerializeField] Transform cameraRoot1;
    [SerializeField] Transform cameraRoot2;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float lookDistance;
    [SerializeField] private CinemachineVirtualCamera incingScene;

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

    private void Update()
    {
        
        Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;
        lookPoint.y = 0f; // ĳ���Ͱ� �ٶ󺸰� �ִ� ��
        transform.LookAt(lookPoint);
    }
    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;   // ���� ������
        xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;   // �� �Ʒ� ** ���̳ʽ��� ����� �ݴ�� �ؾ��Ѵ�.
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);  // Ŭ������� ���а������ִµ�, �� �Ʒ��� �ּ� 80�� ������ �� �� �ֵ���

        cameraRoot1.rotation = Quaternion.Euler(xRotation, yRotation, 0);   // ī�޶� �ٶ󺸴� ��ġ /ī�޶� ��Ʈ�������� ��ƾ� �ϱ⶧���� local�� ������ Rotation�� ���
        cameraRoot2.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    private void OnIncing()
    {
        if (incingScene.Priority < 15)
            incingScene.Priority = 15;
        else
            incingScene.Priority = 9;

    }
    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }

}
