using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class follwing_player : MonoBehaviour
{
    //플레이어들
    GameObject target1;
    GameObject target2;
    GameObject target3;
    GameObject target4;

    //데이터
    NewGameManager total_GM;
    DataManager data_DM;

    //게임시작 확인
    canvas_script can;

    //캐릭터들 중심점 찾기 위함
    float t_x;
    float t_z;

    //카메라 값들
    float pix_t_y = 0.0f;
    float pix_t_z = 0.0f;

    //카메라 기본 위치값들
    float offsetX;
    float offsetY;
    float offsetZ;
    //플레이어수 나눗셈용
    int dead_number_for_devide = 4;

    //라운드 종료시 카메라 시점
    Vector3 done_after_pos;
    //라운드 종료 대기 시간
    float DelayTime;
    //라운드 종료 카메라 위치 시간
    float done_time = 0.0f;

    //플레이어 남은숫자
    int player_number = 4;
    int live_player1 = 1;
    int live_player2 = 2;
    int live_player3 = 3;
    int live_player4 = 4;

    //플레이어 추락 확인
    public int dead_number = 0;
    bool dead_check = false;    
    //라운드 횟수 확인
    int done_total_number = 0;    
    //라운드 종료 확인
    bool done_cam_pos_check = false;

    void Start()
    {
        target1 = GameObject.FindWithTag("Player1");
        target2 = GameObject.FindWithTag("Player2");
        target3 = GameObject.FindWithTag("Player3");
        target4 = GameObject.FindWithTag("Player4");
                
        total_GM = GameObject.FindWithTag("NewGameManager").GetComponent<NewGameManager>();
        data_DM = GameObject.Find("DataManager").GetComponent<DataManager>();

        can = GameObject.FindWithTag("canvas_DM").GetComponent<canvas_script>();

        //각 맵 카메라 구도(위치)
        if (SceneManager.GetActiveScene().name == "Cube_Test")
        {
            offsetX = -0.5f;
            offsetY = 7.0f;
            offsetZ = -4.0f;            
        }
        else if (SceneManager.GetActiveScene().name == "PirateMap")
        {
            transform.rotation = Quaternion.Euler(45.0f, 0, 0);
            offsetX = 0.0f;
            offsetY = 6.0f;
            offsetZ = -7.0f;            
        }
        else if(SceneManager.GetActiveScene().name == "SpaceMap")
        {
            offsetX = -0.5f;
            offsetY = 5.5f;
            offsetZ = -5.0f;            
        }
        else if (SceneManager.GetActiveScene().name == "ForestMap")
        {
            offsetX = 0.0f;
            offsetY = 8.5f;
            offsetZ = -8.0f;            
        }
        DelayTime = 7.0f;
    }

    void Update()
    {
        //모든 라운드 종료시 결과씬으로 이동
        if(total_GM.map_roof_number == 4)
            SceneManager.LoadScene("Result");

        //캐릭터들 중심점 찾기
        t_x = (target1.transform.position.x + target2.transform.position.x + target3.transform.position.x + target4.transform.position.x) / dead_number_for_devide;
        t_z = (target1.transform.position.z + target2.transform.position.z + target3.transform.position.z + target4.transform.position.z) / dead_number_for_devide;

        Check_Player_Dead();
        Round_Done();
    }
    void Check_Player_Dead()
    {
        //캐릭터별 추락 확인
        if (target1.transform.position.y < -4.5f)
        {
            dead_check = true;
            if (dead_check)
            {
                dead_number++;
                dead_number_for_devide--;
                dead_check = false;
            }
            target1.transform.position = new Vector3(0, 10, 0);
            target1.SetActive(false);
            live_player1 = 0;
        }
        if (target2.transform.position.y < -4.5f)
        {
            dead_check = true;
            if (dead_check)
            {
                dead_number++;
                dead_number_for_devide--;
                dead_check = false;
            }
            target2.transform.position = new Vector3(0, 10, 0);
            target2.SetActive(false);
            live_player2 = 0;
        }
        if (target3.transform.position.y < -4.5f)
        {
            dead_check = true;
            if (dead_check)
            {
                dead_number++;
                dead_number_for_devide--;
                dead_check = false;
            }
            target3.transform.position = new Vector3(0, 10, 0);
            target3.SetActive(false);
            live_player3 = 0;
        }
        if (target4.transform.position.y < -4.5f)
        {
            dead_check = true;
            if (dead_check)
            {
                dead_number++;
                dead_number_for_devide--;
                dead_check = false;
            }
            target4.transform.position = new Vector3(0, 10, 0);
            target4.SetActive(false);
            live_player4 = 0;
        }
    }

    void Round_Done()
    {
        //라운드 종료
        if (player_number == 1)
        {
            can.stop_map = true;
        }

        //1명의 플레이어만 살아남았을때
        if (dead_number == 3)
        {
            data_DM.p1_speed = 0.0f;
            data_DM.p2_speed = 0.0f;
            data_DM.p3_speed = 0.0f;
            data_DM.p4_speed = 0.0f;

            can.stop_map = true;
            done_total_number++;
            Camera_Setting_Playerdone();
            if (done_cam_pos_check)
            {
                done_after_pos = new Vector3(t_x, pix_t_y, t_z - pix_t_z);
                transform.position = Vector3.Lerp(transform.position, done_after_pos, Time.deltaTime * DelayTime);
            }
        }
        //모든 플레이어가 추락했을때
        else if (dead_number == 4)
        {


            can.stop_map = true;
            done_total_number++;
            Camera_Setting_Playerdone();
            if (done_cam_pos_check)
            {
                done_after_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, done_after_pos, Time.deltaTime * DelayTime);
            }
        }
        else
        {
            Vector3 FixedPos = new Vector3(t_x + offsetX, offsetY, t_z + offsetZ);

            transform.position = Vector3.Lerp(transform.position, FixedPos, Time.deltaTime * DelayTime);
        }
    }

    void Camera_Setting_Playerdone()
    {
        if (can.stop_map)
        {
            done_time += Time.deltaTime;
            //라운드 종료되면 캐릭터 줌인
            if (done_time > 2.0f)
            {
                if (SceneManager.GetActiveScene().name == "Cube_Test")
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    offsetX = 0.0f;
                    offsetY = 0.0f;
                    offsetZ = 0.0f;                    
                    done_cam_pos_check = true;

                    pix_t_y = 3.0f;
                    pix_t_z = 1.5f;

                    total_GM.map_select_number = 1;
                }
                if (SceneManager.GetActiveScene().name == "PirateMap")
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);                    
                    offsetX = 0.0f;
                    offsetY = 0.0f;
                    offsetZ = 0.0f;                    

                    pix_t_y = 1.0f;
                    pix_t_z = 1.2f;
                    done_cam_pos_check = true;

                    total_GM.map_select_number = 2;
                }
                if (SceneManager.GetActiveScene().name == "SpaceMap")
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    offsetX = 0.0f;
                    offsetY = 0.0f;
                    offsetZ = 0.0f;                    
                    done_cam_pos_check = true;

                    pix_t_y = 0.6f;
                    pix_t_z = 1.3f;

                    total_GM.map_select_number = 0;
                }
                if (SceneManager.GetActiveScene().name == "ForestMap")
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    offsetX = 0.0f;
                    offsetY = 0.0f;
                    offsetZ = 0.0f;                    
                    done_cam_pos_check = true;

                    pix_t_y = 4.5f;
                    pix_t_z = 1.3f;

                    total_GM.map_select_number = 3;
                }
                //라운드 종료후 6초가 지나면 다음 씬으로 이동
                if (done_time > 6.0f)
                {
                    can.stop_map = false;
                    data_DM.p1_speed = 2.0f;
                    data_DM.p2_speed = 2.0f;
                    data_DM.p3_speed = 2.0f;
                    data_DM.p4_speed = 2.0f;

                    total_GM.before_time = 0.0f;
                    total_GM.before_start_state = false;
                    total_GM.map_roof_number++;
                    done_cam_pos_check = false;
                    SceneManager.LoadScene("Loading");
                }
            }            
        }
    }    
}
