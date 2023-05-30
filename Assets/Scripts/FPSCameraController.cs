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
    // 카메라는 업데이트에 구현하지않기 국룰로 LateUpdate에 구현하기
    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;   // 왼쪽 오른쪽
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;   // 위 아래 ** 마이너스로 해줘야 반대로 해야한다.
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);  // 클램프라는 수학공식이있는데, 위 아래를 최소 80도 까지만 볼 수 있도록

        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
