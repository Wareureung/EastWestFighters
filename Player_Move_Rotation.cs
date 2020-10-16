using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    //입력
    float h;
    float v;
    
    //이동
    Vector3 move;
    float movespeed = 2f;

    //회전
    float turnAmount;
    float rotspeed = 280f;

    void Update()
    {
        Player_Move();
    }

    void Player_Move()
    {
        //컨트롤러 값
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //움직이기 위한 조건들(땅 착지여부, 공격상태, 방어상태, 게임시작 확인)
        if (conditions_for_movement)
        {
            //입력값
            move = (Vector3.forward * v) + (Vector3.right * h);
            //월드좌표 -> 로컬좌표
            move = transform.InverseTransformDirection(move);
            //이동
            transform.Translate(move * movespeed * Time.deltaTime);

            //회전값
            turnAmount = Mathf.Atan2(move.x, move.z);
            //회전
            transform.Rotate(0, turnAmount * rotspeed * Time.deltaTime, 0);
        }
    }
}