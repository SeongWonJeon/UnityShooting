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
        // 월드기준 움직임
        // controller.Move(moveDir * moveSpeed * Time.deltaTime); // 위치와 단위시간으로 이동 구현 이동할 때 장애물이 있어도 스무스하게 지나갈 수 있다.\
        
        // 로컬기준 움직임
        if (moveDir.magnitude <= 0) // 안움직였을 때
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.5f);     // 지금수치, 목표속력의, % 의 순서로하면 점차 떨어지도록 할 수 있다
        }
        else if (isWalking) // 움직이고 c를 눌렀을 때
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);         // 점점 변하는 순차적으로 변하도록
                                                                        // Mathf.Lerp();
        }
        else if (isRuning)  // Shift를 누르고 뛸 때
        {
            moveSpeed = Mathf.Lerp(moveSpeed, fastRunSpeed, 0.5f);
        }
        else // 기본걸음걸이 속도로 걸을 때
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
        ySpeed += Physics.gravity.y * Time.deltaTime;       // 가속력에 시간을 곱하며 속력이 
        if (isGrounded && ySpeed < 0)  // 땅에 충돌하면 땅으로 잡아당기는 속도를 0으로, ySpeed가 0보다 작을때(떨어지고 있을 때만)
            ySpeed = -1;

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);      // 중력을 구현한다 그랬을 때는 등가속운동
    }
    private void OnJump(InputValue value)
    {
        if (isGrounded)
            ySpeed = jumpSpeed;
    }

    private void GroundChack()
    {
        RaycastHit hit;
        // 아래방향으로 쐈는데 부딪히면 트루 안부딪히면 false로 반환하도록
        isGrounded = Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.51f);  // 플레이어의 1만큼 위의 위치에서, 어느정도 둘레로 쏠건지, 어느방향으로, 얼마나 쓸건지
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
