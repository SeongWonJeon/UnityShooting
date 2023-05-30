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
        lookPoint.y = 0f; // 캐릭터가 바라보고 있는 곳
        transform.LookAt(lookPoint);
    }
    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;   // 왼쪽 오른쪽
        xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;   // 위 아래 ** 마이너스로 해줘야 반대로 해야한다.
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);  // 클램프라는 수학공식이있는데, 위 아래를 최소 80도 까지만 볼 수 있도록

        cameraRoot1.rotation = Quaternion.Euler(xRotation, yRotation, 0);   // 카메라가 바라보는 위치 /카메라 루트기준으로 잡아야 하기때문에 local을 제거한 Rotation을 사용
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
