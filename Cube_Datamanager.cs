using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_Datamanager : MonoBehaviour
{
    //회전할 큐브 축
    GameObject pivot_cube;
    cube_isTrigger pivot_script;

    canvas_script Cavas_DM;

    //회전속도 및 각도
    public float rot_speed;

    //회전하기전 색변경
    public float rot_before_time = 0.0f;
    float real_before_rot_time = 5.0f;
    float red_zone_time = 2.0f;
    int child_count;
    
    //회전축 회전여부
    public bool Roll_ = false;
    public bool Pitch_ = false;
    public bool Yaw_ = false;

    //전체 회전
    public bool amirot;

    //회전 순서
    int rot_number = 0;

    //회전각도 체크
    bool rotation_call_one;

    //시간
    public float check_time = 0.0f;
    float trigger_time = 5.0f;

    void Start()
    {
        Cavas_DM = GameObject.FindWithTag("canvas_DM").GetComponent<canvas_script>();

        rot_speed = 45.0f;

        amirot = false;
    }

    void Update()
    {
        if (Cavas_DM.doplay && !Cavas_DM.stop_map)
        {
            check_time += Time.deltaTime;

            //회전할 축 이름 입력
            if (check_time >= trigger_time && rot_number == 0)
                Rotate_End("Cube_Pivot_Pitch4");

            if (check_time > trigger_time && rot_number == 1)
                Rotate_Middle("Cube_Pivot_Pitch2");

            if (check_time > trigger_time && rot_number == 2)
                Rotate_Middle("Cube_Pivot_Roll2");

            if (check_time > trigger_time && rot_number == 3)
                Rotate_End("Cube_Pivot_Roll1");

            if (check_time > trigger_time && rot_number == 4)
                Rotate_End("Cube_Pivot_Roll1");
        }
    }

    //중간축 회전
    void Rotate_Middle(string pivot_name)
    {
        //큐브 지정 및 스크립트 지정
        pivot_cube = GameObject.Find(pivot_name);
        pivot_script = pivot_cube.GetComponent<cube_isTrigger>();

        //계속 회전할 수 있게끔 (한방향으로)
        if (rotation_call_one == false)
        {
            if (pivot_script.rotation_limit >= 360)
                pivot_script.rotation_limit = 0f;
            else
                pivot_script.rotation_limit += 90f;

            rotation_call_one = true;
        }

        //Pitch, Roll 구분
        if (pivot_cube.name.Substring(11, 1) == "P")
        {
            Pitch_ = true;
            if (pivot_cube.transform.rotation.eulerAngles.x < pivot_script.rotation_limit)
                pivot_script.amirot = 1;
            if (pivot_cube.transform.rotation.eulerAngles.x >= pivot_script.rotation_limit - 1)
                pivot_script.amirot = 2;
        }
        if (pivot_cube.name.Substring(11, 1) == "R")
        {
            Roll_ = true;
            if (pivot_cube.transform.rotation.eulerAngles.z < pivot_script.rotation_limit)
                pivot_script.amirot = 1;
            if (pivot_cube.transform.rotation.eulerAngles.z >= pivot_script.rotation_limit - 1)
                pivot_script.amirot = 2;
        }

        //회전 끝
        if (pivot_script.imgood)
        {
            rotation_call_one = false;
            pivot_script.imgood = false;
            pivot_script.amirot = 0;                        
            rot_number++;
            rot_before_time = 0.0f;
            check_time = 0.0f;
        }
        //큐브 회전하기, 큐브 회전하기 전 색상변경
        //중간축 확인
        if (pivot_script.amirot == 1)
        {
            rot_before_time += Time.deltaTime;
            if (rot_before_time > red_zone_time)
            {
                pivot_cube.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                child_count = pivot_cube.transform.childCount;

                for (int i = 0; i < child_count; i++)
                {
                    pivot_cube.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }

                if (pivot_cube.name.Substring(11, 1) == "P")
                    pivot_cube.transform.Rotate(Vector3.right * rot_speed * Time.deltaTime, Space.Self);
                if (pivot_cube.name.Substring(11, 1) == "R")
                    pivot_cube.transform.Rotate(Vector3.forward * rot_speed * Time.deltaTime, Space.Self);
            }
            else if (rot_before_time < real_before_rot_time)
            {
                pivot_cube.GetComponent<MeshRenderer>().material.color = new Color(1, 0.2f, 0.2f);
                child_count = pivot_cube.transform.childCount;

                for (int i = 0; i < child_count; i++)
                {
                    pivot_cube.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(1, 0.2f, 0.2f);
                }
            }
        }
    }

    //끝 축 회전
    void Rotate_End(string pivot_name)
    {
        pivot_cube = GameObject.Find(pivot_name);
        pivot_script = pivot_cube.GetComponent<cube_isTrigger>();

        if (rotation_call_one == false)
        {
            //계속 회전할 수 있게끔 (한방향으로)
            if (pivot_script.rotation_limit >= 360)
                pivot_script.rotation_limit = 0f;
            else
                pivot_script.rotation_limit += 90f;

            rotation_call_one = true;
        }

        //큐브 돌기전 축에다가 자식큐브로 넣음
        if (pivot_cube.name.Substring(11, 1) == "P")
        {
            Pitch_ = true;

            if (pivot_cube.transform.rotation.eulerAngles.x < pivot_script.rotation_limit)
                pivot_script.amirot = 3;
            //큐브 돌고나서 축틀어짐 수정
            if (pivot_cube.transform.rotation.eulerAngles.x >= pivot_script.rotation_limit - 1)
                pivot_script.amirot = 4;
        }
        if (pivot_cube.name.Substring(11, 1) == "R")
        {
            Roll_ = true;

            if (pivot_cube.transform.rotation.eulerAngles.z < pivot_script.rotation_limit)
                pivot_script.amirot = 3;
            //큐브 돌고나서 축틀어짐 수정
            if (pivot_cube.transform.rotation.eulerAngles.z >= pivot_script.rotation_limit - 1)
                pivot_script.amirot = 4;
        }
        //다 돌고 다음으로 넘어가기전
        if (pivot_script.imgood)
        {
            rotation_call_one = false;
            pivot_script.imgood = false;
            pivot_script.amirot = 0;            
            rot_number++;
            rot_before_time = 0.0f;
            check_time = 0.0f;
        }
        //큐브 회전하기, 큐브 회전하기 전 색상변경
        //끝축 확인
        if (pivot_script.amirot == 3)
        {
            rot_before_time += Time.deltaTime;
            if (rot_before_time > red_zone_time)
            {
                pivot_cube.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                child_count = pivot_cube.transform.childCount;

                for (int i = 0; i < child_count; i++)
                {
                    pivot_cube.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }

                if (pivot_cube.name.Substring(11, 1) == "P")
                    pivot_cube.transform.Rotate(Vector3.right * rot_speed * Time.deltaTime, Space.Self);
                if (pivot_cube.name.Substring(11, 1) == "R")
                    pivot_cube.transform.Rotate(Vector3.forward * rot_speed * Time.deltaTime, Space.Self);
            }
            else if (rot_before_time < real_before_rot_time)
            {
                pivot_cube.GetComponent<MeshRenderer>().material.color = new Color(1, 0.2f, 0.2f);
                child_count = pivot_cube.transform.childCount;

                for (int i = 0; i < child_count; i++)
                {
                    pivot_cube.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(1, 0.2f, 0.2f);
                }
            }
        }
    }
}
