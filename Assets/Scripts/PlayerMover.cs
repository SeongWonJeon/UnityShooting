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
        // 월드기준 움직임
        // controller.Move(moveDir * moveSpeed * Time.deltaTime); // 위치와 단위시간으로 이동 구현 이동할 때 장애물이 있어도 스무스하게 지나갈 수 있다.\
        
        // 로컬기준 움직임
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

        if (GroundChack() && ySpeed < 0)  // 땅에 충돌하면 땅으로 잡아당기는 속도를 0으로, ySpeed가 0보다 작을때(떨어지고 있을 때만)
            ySpeed = -0.5f;
        else
            ySpeed += Physics.gravity.y * Time.deltaTime;       // 가속력에 시간을 곱하며 속력이 

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);      // 중력을 구현한다 그랬을 때는 등가속운동
    }
    private void OnJump(InputValue value)
    {
        if (GroundChack())
            ySpeed = jumpSpeed;
    }

    private bool GroundChack()
    {
        RaycastHit hit;
        // 아래방향으로 쐈는데 부딪히면 트루 안부딪히면 false로 반환하도록
        return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.51f);  // 플레이어의 1만큼 위의 위치에서, 어느정도 둘레로 쏠건지, 어느방향으로, 얼마나 쓸건지
    }
}
