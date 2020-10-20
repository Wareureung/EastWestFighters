using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_isTrigger : MonoBehaviour
{
    //큐브조각
    GameObject[] Cube00 = new GameObject[93];
    string cube_name;

    //게임 시작후 큐브 회전하기 위함
    cube_Datamanager cube_manager;

    //중심축 큐브 제외한 큐브들이 모여있는 곳
    GameObject CUBE;

    //큐브, 큐브위치 들어갈 배열
    GameObject[] cube_array = new GameObject[24];
    Vector3[] cube_before_pos = new Vector3[24];

    //Pitch, Roll 구분
    public int amirot;
    //회전 상태 확인
    public bool imgood;

    //회전 보정용
    float set_rot_value_x;
    float set_rot_value_y;
    float set_rot_value_z;

    //큐브 갯수값
    int cube_index;

    //회전값 축적
    public float rotation_limit;

    void Start()
    {
        cube_manager = GameObject.Find("CubeDataManager").GetComponent<cube_Datamanager>();
        CUBE = GameObject.Find("AllCube");

        //모든큐브
        for (int i = 0; i < 93; i++)
        {
            cube_name = string.Format("Cube_{0:D2}", i);    //Cube_00 형태 받아옴
            Cube00[i] = GameObject.Find(cube_name);
        };

        //해당 큐브
        for (int i = 0; i < 24; i++)
        {
            cube_array[i] = null;
            cube_before_pos[i] = Vector3.zero;
        }

        amirot = 0;
        imgood = false;
        cube_index = 0;

        //각 축마다 얼마나 회전했는지에 대한 축적치
        rotation_limit = 0f;
    }

    void Update()
    {
        Pitch_Rotate_Pix();
        Roll_Rotate_Pix();
    }

    //Pitch 회전후 틀어짐 수정
    void Pitch_Rotate_Pix()
    {
        if (amirot == 2)
        {
            //Trigger로 인한 사용량 줄이기 위해 끄고/켜기
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;

            for (int i = 0; i < 16; i++)
            {
                //----------------+++++++++++++++++++++----------------//
                //cube 회전후 포지션 틀어짐 수정      
                //x 양수일때 
                if (cube_array[i].transform.position.x > 0)
                {
                    if (cube_array[i].transform.position.x / 2 >= 1 || cube_array[i].transform.position.x / 2 > 0.7f)
                        cube_array[i].transform.position = new Vector3(2.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);
                    if (cube_array[i].transform.position.x / 2 < 0.7f && cube_array[i].transform.position.x > 0.1f)
                        cube_array[i].transform.position = new Vector3(1.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);
                    if (cube_array[i].transform.position.x / 2 < 0.1f)
                        cube_array[i].transform.position = new Vector3(0.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);

                }
                //x 음수일때
                if (cube_array[i].transform.position.x < 0)
                {
                    set_rot_value_x = cube_array[i].transform.position.x / 2;

                    if (Mathf.Abs(set_rot_value_x) >= 1 || Mathf.Abs(set_rot_value_x) > 0.7f)
                        cube_array[i].transform.position = new Vector3(-2.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);
                    if (Mathf.Abs(set_rot_value_x) < 0.7f && Mathf.Abs(set_rot_value_x) > 0.1f)
                        cube_array[i].transform.position = new Vector3(-1.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);
                    if (Mathf.Abs(set_rot_value_x) < 0.1f)
                        cube_array[i].transform.position = new Vector3(0.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);

                }

                //y 양수일때               
                if (cube_array[i].transform.position.y > 0)
                {
                    if (cube_array[i].transform.position.y / 2 >= 1 || cube_array[i].transform.position.y / 2 > 0.7f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, 2.0f, cube_array[i].transform.position.z);
                    if (cube_array[i].transform.position.y / 2 < 0.7f && cube_array[i].transform.position.y > 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, 1.0f, cube_array[i].transform.position.z);
                    if (cube_array[i].transform.position.y / 2 < 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, 0.0f, cube_array[i].transform.position.z);

                }
                //y 음수일때
                if (cube_array[i].transform.position.y < 0)
                {
                    set_rot_value_y = cube_array[i].transform.position.y / 2;

                    if (Mathf.Abs(set_rot_value_y) >= 1 || Mathf.Abs(set_rot_value_y) > 0.7f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, -2.0f, cube_array[i].transform.position.z);
                    if (Mathf.Abs(set_rot_value_y) < 0.7f && Mathf.Abs(set_rot_value_y) > 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, -1.0f, cube_array[i].transform.position.z);
                    if (Mathf.Abs(set_rot_value_y) < 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, 0.0f, cube_array[i].transform.position.z);

                }

                //z 양수일때
                if (cube_array[i].transform.position.z > 0)
                {
                    if (cube_array[i].transform.position.z / 2 >= 1 || cube_array[i].transform.position.z / 2 > 0.7)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, 2.0f);
                    if (cube_array[i].transform.position.z / 2 < 0.7f && cube_array[i].transform.position.z / 2 > 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, 1.0f);
                    if (cube_array[i].transform.position.z / 2 < 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, 0.0f);
                }
                //z 음수일때
                if (cube_array[i].transform.position.z < 0)
                {
                    set_rot_value_z = cube_array[i].transform.position.z / 2;

                    if (Mathf.Abs(set_rot_value_z) >= 1 || Mathf.Abs(set_rot_value_z) > 0.7)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, -2.0f);
                    if (Mathf.Abs(set_rot_value_z) < 0.7f && Mathf.Abs(set_rot_value_z) > 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, -1.0f);
                    if (Mathf.Abs(set_rot_value_z) < 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, 0.0f);
                }
                //----------------+++++++++++++++++++++----------------//
                //텍스쳐 돌아가는거 방지용
                if (cube_manager.Pitch_)
                    cube_array[i].transform.rotation = Quaternion.Euler(cube_array[i].transform.rotation.x + rotation_limit, cube_array[i].transform.rotation.y, cube_array[i].transform.rotation.z);
                if (cube_manager.Roll_)
                    cube_array[i].transform.rotation = Quaternion.Euler(cube_array[i].transform.rotation.x, cube_array[i].transform.rotation.y, cube_array[i].transform.rotation.z + rotation_limit);
                cube_array[i].transform.SetParent(CUBE.transform);
                cube_array[i] = null;
            }
            imgood = true;
            cube_manager.Pitch_ = false;
            cube_manager.Roll_ = false;
            cube_index = 0;
        }
    }

    //Roll 회전후 틀어짐 수정
    void Roll_Rotate_Pix()
    {
        if (amirot == 4)
        {
            //Trigger로 인한 사용량 줄이기 위해 끄고/켜기
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;

            for (int i = 0; i < 24; i++)
            {
                //----------------+++++++++++++++++++++----------------//
                //cube 회전후 포지션 틀어짐 수정      
                //x 양수일때 
                if (cube_array[i].transform.position.x > 0)
                {
                    if (cube_array[i].transform.position.x / 2 >= 1 || cube_array[i].transform.position.x / 2 > 0.7f)
                        cube_array[i].transform.position = new Vector3(2.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);
                    if (cube_array[i].transform.position.x / 2 < 0.7f && cube_array[i].transform.position.x > 0.1f)
                        cube_array[i].transform.position = new Vector3(1.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);
                    if (cube_array[i].transform.position.x / 2 < 0.1f)
                        cube_array[i].transform.position = new Vector3(0.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);

                }
                //x 음수일때
                if (cube_array[i].transform.position.x < 0)
                {
                    set_rot_value_x = cube_array[i].transform.position.x / 2;

                    if (Mathf.Abs(set_rot_value_x) >= 1 || Mathf.Abs(set_rot_value_x) > 0.7f)
                        cube_array[i].transform.position = new Vector3(-2.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);
                    if (Mathf.Abs(set_rot_value_x) < 0.7f && Mathf.Abs(set_rot_value_x) > 0.1f)
                        cube_array[i].transform.position = new Vector3(-1.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);
                    if (Mathf.Abs(set_rot_value_x) < 0.1f)
                        cube_array[i].transform.position = new Vector3(0.0f, cube_array[i].transform.position.y, cube_array[i].transform.position.z);

                }

                //y 양수일때               
                if (cube_array[i].transform.position.y > 0)
                {
                    if (cube_array[i].transform.position.y / 2 >= 1 || cube_array[i].transform.position.y / 2 > 0.7f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, 2.0f, cube_array[i].transform.position.z);
                    if (cube_array[i].transform.position.y / 2 < 0.7f && cube_array[i].transform.position.y > 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, 1.0f, cube_array[i].transform.position.z);
                    if (cube_array[i].transform.position.y / 2 < 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, 0.0f, cube_array[i].transform.position.z);

                }
                //y 음수일때
                if (cube_array[i].transform.position.y < 0)
                {
                    set_rot_value_y = cube_array[i].transform.position.y / 2;

                    if (Mathf.Abs(set_rot_value_y) >= 1 || Mathf.Abs(set_rot_value_y) > 0.7f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, -2.0f, cube_array[i].transform.position.z);
                    if (Mathf.Abs(set_rot_value_y) < 0.7f && Mathf.Abs(set_rot_value_y) > 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, -1.0f, cube_array[i].transform.position.z);
                    if (Mathf.Abs(set_rot_value_y) < 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, 0.0f, cube_array[i].transform.position.z);

                }

                //z 양수일때
                if (cube_array[i].transform.position.z > 0)
                {
                    if (cube_array[i].transform.position.z / 2 >= 1 || cube_array[i].transform.position.z / 2 > 0.7)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, 2.0f);
                    if (cube_array[i].transform.position.z / 2 < 0.7f && cube_array[i].transform.position.z / 2 > 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, 1.0f);
                    if (cube_array[i].transform.position.z / 2 < 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, 0.0f);
                }
                //z 음수일때
                if (cube_array[i].transform.position.z < 0)
                {
                    set_rot_value_z = cube_array[i].transform.position.z / 2;

                    if (Mathf.Abs(set_rot_value_z) >= 1 || Mathf.Abs(set_rot_value_z) > 0.7)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, -2.0f);
                    if (Mathf.Abs(set_rot_value_z) < 0.7f && Mathf.Abs(set_rot_value_z) > 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, -1.0f);
                    if (Mathf.Abs(set_rot_value_z) < 0.1f)
                        cube_array[i].transform.position = new Vector3(cube_array[i].transform.position.x, cube_array[i].transform.position.y, 0.0f);
                }
                //----------------+++++++++++++++++++++----------------//
                //텍스쳐 돌아가는거 방지용
                if (cube_manager.Pitch_)
                    cube_array[i].transform.rotation = Quaternion.Euler(cube_array[i].transform.rotation.x + rotation_limit, cube_array[i].transform.rotation.y, cube_array[i].transform.rotation.z);
                if (cube_manager.Roll_)
                    cube_array[i].transform.rotation = Quaternion.Euler(cube_array[i].transform.rotation.x, cube_array[i].transform.rotation.y, cube_array[i].transform.rotation.z + rotation_limit);
                cube_array[i].transform.SetParent(CUBE.transform);
                cube_array[i] = null;
            }
            imgood = true;
            cube_manager.Pitch_ = false;
            cube_manager.Roll_ = false;
            cube_index = 0;
        }
    }

    void OnTriggerStay(Collider other)
    {   
        //가운데 회전축
        if (amirot == 1)
        {
            //모든 큐브 탐색
            for (int i = 0; i < 93; i++)
            {
                //해당 큐브 구분
                if (other.name == Cube00[i].name)
                {
                    //해당 큐브 다 찾으면 탐색 종료
                    if (cube_index > 16)
                    {
                        i = 96;
                    }
                    //해당 큐브들 부모오브젝트 변경
                    else if (cube_array[cube_index] == null && cube_index < 16)
                    {
                        cube_array[cube_index] = Cube00[i];
                        cube_array[cube_index].transform.SetParent(this.transform);
                        cube_index++;
                    }
                }
            }
            //Trigger로 인한 사용량 줄이기 위해 끄고/켜기
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }

        //끝 회전축
        if (amirot == 3)
        {
            //모든 큐브 탐색
            for (int i = 0; i < 93; i++)
            {
                //해당 큐브 구분
                if (other.name == Cube00[i].name)
                {
                    //해당 큐브 다 찾으면 탐색 종료
                    if (cube_index > 23)
                    {
                        i = 96;
                    }
                    //해당 큐브들 부모오브젝트 변경
                    else if (cube_array[cube_index] == null && cube_index < 24)
                    {
                        cube_array[cube_index] = Cube00[i];
                        cube_before_pos[cube_index] = cube_array[cube_index].transform.position;
                        cube_array[cube_index].transform.SetParent(this.transform);
                        cube_index++;
                    }
                }
            }
            //Trigger로 인한 사용량 줄이기 위해 끄고/켜기
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
