using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class new_Title : MonoBehaviour
{
    //맵 이미지 전체를 움직일 오브젝트
    GameObject All_Map;

    //각 맵 이미지들
    GameObject Map_Ice;
    GameObject Map_Pirates;
    GameObject Map_Forest;
    GameObject Map_Space;

    //선택 했을때 나타날 블러 이미지들
    GameObject Blur_ice;
    GameObject Blur_Pirates;
    GameObject Blur_Forest;
    GameObject Blur_Space;

    //맵 선택했을때 알맞은 이미지 불러오기용
    Image Fade_img;

    //맵 비디오 재생
    //video_play라는 자체 제작 스크립트 사용
    video_play ice_video;
    video_play sea_video;
    video_play forest_video;
    video_play space_video;

    //어떤 맵인지 구별용
    int Map_number = 0;
    //좌우 이동 구분하기 위함
    int Map_select = 0;
    //맵 이동후 고정용
    int Map_move = 250;
    //좌,우로 이동량
    float Max_move = 0;
    //움직이고 있는지 확인
    int amimoveing = 0;
    //맵 선택완료 확인
    bool map_select_done = false;
    //맵 이동속도
    float Map_move_speed = 0;

    void Start()
    {
        All_Map = GameObject.FindWithTag("All_Map");

        Map_Ice = GameObject.FindWithTag("Map_Ice");
        Map_Pirates = GameObject.FindWithTag("Map_Pirates");
        Map_Forest = GameObject.FindWithTag("Map_Forest");
        Map_Space = GameObject.FindWithTag("Map_Space");

        Blur_ice = GameObject.Find("Map_Ice_Blur");
        Blur_Pirates = GameObject.Find("Map_Pirates_Blur");
        Blur_Forest = GameObject.Find("Map_Forest_Blur");
        Blur_Space = GameObject.Find("Map_Space_Blur");

        ice_video = GameObject.Find("ice_video").GetComponent<video_play>();
        sea_video = GameObject.Find("sea_video").GetComponent<video_play>();
        forest_video = GameObject.Find("forest_video").GetComponent<video_play>();
        space_video = GameObject.Find("space_video").GetComponent<video_play>();
    }

    void Update()
    {
        Map_Select();
        Map_Select_Done();
    }

    //맵선택
    void Map_Select()
    {
        //1P가 맵을 선택함
        //이미지가 슬라이드 형태로 넘어가는 도중 변경 불가(이미지가 멈춘 상태에서만 좌,우 이동 가능)
        if ((Input.GetAxisRaw("Horizontal_Dpad_X") == 1) && Map_number < 3 && amimoveing != 2 && Map_move_speed == 0 && !joy1dpad)
        {
            joy1dpad = true;
            Map_select = 1;
            Map_number++;
            Max_move += 250.0f;
        }
        if ((Input.GetAxisRaw("Horizontal_Dpad_X") == -1) && Map_number > 0 && amimoveing != 1 && Map_move_speed == 0 && !joy1dpad)
        {
            joy1dpad = true;
            Map_select = 2;
            Map_number--;
            Max_move -= 250.0f;
        }
        //입력이 없을때 멈추기 위함
        if (Input.GetAxisRaw("Horizontal_Dpad_X") == 0)
            joy1dpad = false;

        //우측이동
        if (Map_select == 1)
        {
            //우측이동 입력 확인
            if (All_Map.transform.position.x > -Max_move)
            {
                //이동속도 설정
                Map_move_speed = 1.2f;
                //우측으로 이동
                All_Map.transform.Translate(new Vector3(-Map_move, 0, 0) * Map_move_speed * Time.deltaTime);
            }
            else
            {
                //완료되면 이동속도 제거
                Map_move_speed = 0.0f;
                
                //이동완료후 뒤틀림 보정용
                if (All_Map.transform.position.x % 10 < 0)
                {                   
                    All_Map.transform.position = new Vector3(-Max_move, All_Map.transform.position.y, All_Map.transform.position.z);
                }
            }
        }
        //좌측이동
        if (Map_select == 2)
        {
            //좌측이동 입력 확인
            if (All_Map.transform.position.x < -Max_move)
            {
                Map_move_speed = 1.2f;
                All_Map.transform.Translate(new Vector3(Map_move, 0, 0) * Map_move_speed * Time.deltaTime);
            }
            else
            {
                Map_move_speed = 0.0f;
                if (All_Map.transform.position.x % 10 < 0)
                {
                    All_Map.transform.position = new Vector3(-Max_move, All_Map.transform.position.y, All_Map.transform.position.z);
                }
            }
        }

        //1번맵
        if (Map_number == 0)
        {
            ice_video.go_play = true;
            sea_video.go_play = false;
            forest_video.go_play = false;
            space_video.go_play = false;
        }
        //2번맵
        if (Map_number == 1)
        {
            ice_video.go_play = false;
            sea_video.go_play = true;
            forest_video.go_play = false;
            space_video.go_play = false;
        }
        //3번맵
        if (Map_number == 2)
        {
            ice_video.go_play = false;
            sea_video.go_play = false;
            forest_video.go_play = true;
            space_video.go_play = false;
        }
        //4번맵
        if (Map_number == 3)
        {
            ice_video.go_play = false;
            sea_video.go_play = false;
            forest_video.go_play = false;
            space_video.go_play = true;
        }

        //맵 선택완료
        if ((Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            map_select_done = true;
        }

        //맵을 선택했을때 바로 넘어가지 않고 해당하는 블러 이미지 불러오기용
        if (map_select_done)
        {
            if (Map_number == 0)
            {
                //해당하는 블러 이미지를 불러옴
                Blur_ice.SetActive(true);
                Fade_img = Blur_ice.GetComponent<Image>();
                //블러 이미지 불러올때 Fade_In 효과
                Image_Alpha();
            }
            if (Map_number == 1)
            {
                Blur_Pirates.SetActive(true);
                Fade_img = Blur_Pirates.GetComponent<Image>();
                Image_Alpha();
            }
            if (Map_number == 2)
            {
                Blur_Forest.SetActive(true);
                Fade_img = Blur_Forest.GetComponent<Image>();
                Image_Alpha();
            }
            if (Map_number == 3)
            {
                Blur_Space.SetActive(true);
                Fade_img = Blur_Space.GetComponent<Image>();
                Image_Alpha();
            }
        }

    }

    //선택완료시 불러오는 함수
    void Map_Select_Done()
    {
        //블러 상태에서 한번더 완료 버튼을 누르면 로딩씬으로 넘어감
        if ((Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            SceneManager.LoadScene("Loading");
        }
        //블러 상태에서 취소 버튼 누르면 블러 이미지 선택전으로 돌아감
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            map_select_done = false;

            //블러효과 주기위한 값들 초기화
            Blur_ice.SetActive(false);
            Blur_Pirates.SetActive(false);
            Blur_Forest.SetActive(false);
            Blur_Space.SetActive(false);

            Fade_img.color = Fade_Color;
            Fade_Color = new Color(0, 0, 0, 0);
            Alpha_value = 0.0f;
        }
    }
}