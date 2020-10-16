using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    //�Է�
    float h;
    float v;
    
    //�̵�
    Vector3 move;
    float movespeed = 2f;

    //ȸ��
    float turnAmount;
    float rotspeed = 280f;

    void Update()
    {
        Player_Move();
    }

    void Player_Move()
    {
        //��Ʈ�ѷ� ��
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //�����̱� ���� ���ǵ�(�� ��������, ���ݻ���, ������, ���ӽ��� Ȯ��)
        if (conditions_for_movement)
        {
            //�Է°�
            move = (Vector3.forward * v) + (Vector3.right * h);
            //������ǥ -> ������ǥ
            move = transform.InverseTransformDirection(move);
            //�̵�
            transform.Translate(move * movespeed * Time.deltaTime);

            //ȸ����
            turnAmount = Mathf.Atan2(move.x, move.z);
            //ȸ��
            transform.Rotate(0, turnAmount * rotspeed * Time.deltaTime, 0);
        }
    }
}