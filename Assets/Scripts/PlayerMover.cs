using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;


    private CharacterController controller;
    private Vector3 moveDir;
    private float ySpeed = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    

    private void Move()
    { 
        // ������� ������
        // controller.Move(moveDir * moveSpeed * Time.deltaTime); // ��ġ�� �����ð����� �̵� ���� �̵��� �� ��ֹ��� �־ �������ϰ� ������ �� �ִ�.\
        
        // ���ñ��� ������
        controller.Move(transform.forward * moveDir.z * moveSpeed *  Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed *  Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }
    private void Jump()
    {

        if (GroundChack() && ySpeed < 0)  // ���� �浹�ϸ� ������ ��ƴ��� �ӵ��� 0����, ySpeed�� 0���� ������(�������� ���� ����)
            ySpeed = -0.5f;
        else
            ySpeed += Physics.gravity.y * Time.deltaTime;       // ���ӷ¿� �ð��� ���ϸ� �ӷ��� 

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);      // �߷��� �����Ѵ� �׷��� ���� ��ӿ
    }
    private void OnJump(InputValue value)
    {
        if (GroundChack())
            ySpeed = jumpSpeed;
    }

    private bool GroundChack()
    {
        RaycastHit hit;
        // �Ʒ��������� ���µ� �ε����� Ʈ�� �Ⱥε����� false�� ��ȯ�ϵ���
        return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.51f);  // �÷��̾��� 1��ŭ ���� ��ġ����, ������� �ѷ��� �����, �����������, �󸶳� ������
    }
}
