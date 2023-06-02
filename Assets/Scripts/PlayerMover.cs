using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float fastRunSpeed;
    [SerializeField] float jumpSpeed;


    private CharacterController controller;
    private Animator anim;
    [SerializeField] private Vector3 moveDir;
    private float ySpeed = 0;
    private float moveSpeed;
    private bool isGrounded;
    private bool isWalking;
    private bool isRuning;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        GroundChack();
    }

    private void Move()
    { 
        // ������� ������
        // controller.Move(moveDir * moveSpeed * Time.deltaTime); // ��ġ�� �����ð����� �̵� ���� �̵��� �� ��ֹ��� �־ �������ϰ� ������ �� �ִ�.\
        
        // ���ñ��� ������
        if (moveDir.magnitude <= 0) // �ȿ������� ��
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.5f);     // ���ݼ�ġ, ��ǥ�ӷ���, % �� �������ϸ� ���� ���������� �� �� �ִ�
        }
        else if (isWalking) // �����̰� c�� ������ ��
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);         // ���� ���ϴ� ���������� ���ϵ���
                                                                        // Mathf.Lerp();
        }
        else if (isRuning)  // Shift�� ������ �� ��
        {
            moveSpeed = Mathf.Lerp(moveSpeed, fastRunSpeed, 0.5f);
        }
        else // �⺻�������� �ӵ��� ���� ��
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, 0.5f);
        }

        controller.Move(transform.forward * moveDir.z * moveSpeed *  Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed *  Time.deltaTime);


        anim.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
        anim.SetFloat("YSpeed", moveDir.z, 0.1f, Time.deltaTime);
        anim.SetFloat("Speed", moveSpeed);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);

        
    }
    private void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;       // ���ӷ¿� �ð��� ���ϸ� �ӷ��� 
        if (isGrounded && ySpeed < 0)  // ���� �浹�ϸ� ������ ��ƴ��� �ӵ��� 0����, ySpeed�� 0���� ������(�������� ���� ����)
            ySpeed = -1;

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);      // �߷��� �����Ѵ� �׷��� ���� ��ӿ
    }
    private void OnJump(InputValue value)
    {
        if (isGrounded)
            ySpeed = jumpSpeed;
    }

    private void GroundChack()
    {
        RaycastHit hit;
        // �Ʒ��������� ���µ� �ε����� Ʈ�� �Ⱥε����� false�� ��ȯ�ϵ���
        isGrounded = Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.51f);  // �÷��̾��� 1��ŭ ���� ��ġ����, ������� �ѷ��� �����, �����������, �󸶳� ������
    }
    private void OnWalking(InputValue value)
    {
        isWalking = value.isPressed;
    }

    private void OnRuning(InputValue value)
    {
        isRuning = value.isPressed;
    }
}
