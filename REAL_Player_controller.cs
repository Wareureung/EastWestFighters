using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REAL_Player1_controller : MonoBehaviour
{
    //데이터 매니저
    Player_DataManager PDateManager;

    //게임 시작 확인
    canvas_script canvasDm;

    //AddForce 주기용
    Rigidbody rigid;

    //애니메이터
    Animator my_animator;

    //이동 방향
    Vector3 move;

    //회전
    float turnAmount;

    //입력값들
    float h;
    float v;

    //이동속도
    public float movespeed = 2.0f;
    //회전속도
    float rotspeed = 280.0f;
    //점프값
    float jump_power = 4.5f;
    //이단 점프 방지
    int limit_jump = 0;

    //착지, 점프, 피격, 공격 확인
    bool isGround = false;
    bool isjump = false;    
    bool amihit = false;
    public bool isattack = false;

    //피격시 넘어지기용
    bool goslip = false;

    //버티기
    bool amiendure = false;
    //캐릭터 머리 밟고 점프용
    bool onthecharater = false;

    //공격하기 위한 시간값
    float attack_time_value = 0.0f;

    //점수
    public int player1_score = 0;
    //추락확인
    public bool amireallydead = false;

    void Start()
    {
        PDateManager = GameObject.Find("PlayerDataManager").GetComponent<Player_DataManager>();
        canvasDm = GameObject.FindWithTag("canvas_DM").GetComponent<canvas_script>();
        my_animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        limit_jump = 1;
        DataManager.Instance.PlayerSpeed = 2.0f;
    }

    void Update()
    {
        //이동
        Player_Move();
        //기울어짐 방지
        Player_Rot_Pix();
        //점프 종료 확인
        Player_Jump_Done();
        //점프관련
        Player_Jump_Exception();
        //행동들
        Player_Active();
        //시작 딜레이
        Before_Playing();
    }

    void Player_Move()
    {
        //컨트롤러에서 값 받아옴
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //움직이기 위한 조건들(땅 착지여부, 공격상태, 방어상태, 게임시작 확인)
        if (isGround && !amihit && canvasDm.doplay && !canvasDm.stop_map && !isattack && !amiendure)
        {
            //키반전 조건 부분
            if (DataManager.Instance.p1_ReverseKey == false)
            {
                move = (Vector3.forward * v) + (Vector3.right * h);

            }
            else
                move = (Vector3.forward * h) + (Vector3.right * v);

            //월드좌표 -> 로컬좌표
            move = transform.InverseTransformDirection(move);

            //회전값 받아오기
            turnAmount = Mathf.Atan2(move.x, move.z);
            //회전
            transform.Rotate(0, turnAmount * rotspeed * Time.deltaTime, 0);

            //달릴때만 공격 가능하게
            if (move.magnitude == 0)
                attack_time_value = 0.0f;
            if (move.magnitude > 0)
                attack_time_value += Time.deltaTime;
        }
    }

    void Player_Rot_Pix()
    {
        //기울어졌을때 밑으로 꺼지지 않게 하기 위함
        if (transform.eulerAngles.x > 0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        if (transform.eulerAngles.z > 0)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    void Player_Active()
    {
        //땅에 있을때
        if (isGround)
        {
            limit_jump = 1;
            if (h == 0 || v == 0)
            {
                my_animator.SetInteger("out_endure_check", 2);
                my_animator.SetInteger("out_attack", 1);
            }
            else
            {
                my_animator.SetInteger("out_endure_check", 1);
                my_animator.SetInteger("out_attack", 2);
            }

            if (Input.GetKeyUp(KeyCode.JoystickButton2) && !isattack && !amihit && !isjump)
            {
                my_animator.SetBool("am_i_endure", false);
                amiendure = false;
                DataManager.Instance.p1_speed = 2.0f;
            }

            //아이들 상태에서 공격만 켜지면 가만히 서서 계속 움직이는 버그 생기는데 그거 방지용
            if (my_animator.GetCurrentAnimatorStateInfo(0).IsName("idle_v") && !my_animator.GetBool("go_hit"))
            {
                isattack = false;
                my_animator.SetBool("go_hit", false);
            }

            //일반 점프
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if (limit_jump == 1 && !amiendure)
                {
                    my_animator.SetTrigger("Jump");

                    if (h != 0 || v != 0)
                    {
                        my_animator.SetBool("IsJump", false);
                    }
                    else
                    {
                        my_animator.SetBool("IsJump", true);
                    }                    
                    rigid.AddForce(transform.up * jump_power, ForceMode.Impulse);
                    isGround = false;
                    limit_jump = 0;
                }
            }

            //공격
            if (!amiendure && !isattack && !amihit && !my_animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1") && DataManager.Instance.p1_NonAttack == false)
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                {
                    if (move.magnitude >= 0.98f && attack_time_value > 0.2f)
                    {
                        isattack = true;
                        my_animator.SetBool("go_hit", true);
                    }
                    else
                    {
                        isattack = false;
                        my_animator.SetBool("go_hit", false);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                {
                    DataManager.Instance.p1_speed = 0.0f;
                    my_animator.SetBool("am_i_endure", true);
                    amiendure = true;
                }
                else
                {
                    my_animator.SetBool("IsRun", move.magnitude > 0.2f);
                }
            }
        }
        else
        {
            isGround = false;
            limit_jump = 0;
        }

        //버티기
        if (!amiendure)
        {
            if (amihit)
            {
                my_animator.SetBool("IsJump", false);
                my_animator.SetBool("amIhit", true);
                if (goslip)
                    transform.Translate(Vector3.back * DataManager.Instance.p1_slip_power * Time.deltaTime);
            }
        }
        else if (amiendure)
        {
            amihit = false;
        }

        //공격 확인
        if (!amihit)
        {
            my_animator.SetBool("jump_hit", false);

            if (isattack || isjump)
                transform.Translate(Vector3.forward * DataManager.Instance.p1_speed * Time.deltaTime);
            else
                transform.Translate(move * DataManager.Instance.p1_speed * Time.deltaTime);
        }
    }

    void Before_Playing()
    {
        //기차 다 지나가야 시작
        if (canvasDm.stop_map)
        {
            DataManager.Instance.p1_speed = 0.0f;
            my_animator.SetBool("IsRun", false);
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    void Player_Jump_Exception()
    {
        //점프 도중에 피격 당할 경우
        if (my_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && amihit)
        {
            my_animator.SetBool("jump_hit", true);
        }

        //캐릭터 밟았을때 적 머리 위에서 자동으로 점프 한번함 (일반 점프랑 구분)
        if (!isGround && onthecharater && !amihit)
        {
            if (limit_jump == 1)
            {
                my_animator.SetTrigger("Jump");

                if (h != 0 || v != 0)
                {
                    my_animator.SetBool("IsJump", false);
                }
                else
                {
                    my_animator.SetBool("IsJump", true);
                }
                rigid.AddForce(transform.up * jump_power, ForceMode.Impulse);
                rigid.velocity = new Vector3(0, 0.7f, 0);
                isGround = false;
                limit_jump = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //버티기 아닐때
        if (!amiendure)
        {
            //각 플레이어들과 충돌검사
            if (other.gameObject.tag == "Player1")
            {
                if (PDateManager.Player2_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
                else if (PDateManager.Player3_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
                else if (PDateManager.Player4_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
            }
            if (other.gameObject.tag == "Player2")
            {
                if (PDateManager.Player2_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
                else if (PDateManager.Player3_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
                else if (PDateManager.Player4_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
            }
            if (other.gameObject.tag == "Player3")
            {
                if (PDateManager.Player2_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
                else if (PDateManager.Player3_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
                else if (PDateManager.Player4_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
            }
            if (other.gameObject.tag == "Player4")
            {
                if (PDateManager.Player2_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
                else if (PDateManager.Player3_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
                else if (PDateManager.Player4_Hit_state)
                {
                    transform.LookAt(other.transform.position);
                    amihit = true;
                }
            }
        }

        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            limit_jump = 1;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //캐릭터 머리 밟으면 점프 한번더 되도록
        if (collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
        {
            if ((transform.position.y > PDateManager.Player2.transform.position.y
            || transform.position.y > PDateManager.Player3.transform.position.y
            || transform.position.y > PDateManager.Player4.transform.position.y) && limit_jump == 0)
            {
                limit_jump = 1;
                onthecharater = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            DataManager.Instance.p1_speed = 2.0f;
        }
        onthecharater = false;
    }

    //공격 종료
    public void Hit_Ani_Done()
    {
        isattack = false;
        amihit = false;
        my_animator.SetBool("jump_hit", false);
        my_animator.SetBool("go_hit", false);
    }

    //점프 종료
    public void Player_Jump_Done()
    {
        isjump = false;
    }

    //피격종료(일어나기)
    public void Amihit_false()
    {
        amihit = false;
        my_animator.SetBool("amIhit", false);
        my_animator.SetBool("jump_hit", false);
        my_animator.SetBool("go_hit", false);
    }

    //피격종료(미끄러지기 종료)
    public void Done_Slip()
    {
        my_animator.SetTrigger("Stand_up");
        goslip = false;
        isattack = false;
        move = Vector3.zero;
    }

    //피격확인(미끄러지기 시작)
    public void Go_Slip()
    {        
        goslip = true;
    }
}
