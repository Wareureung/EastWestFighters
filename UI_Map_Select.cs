using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class new_Title : MonoBehaviour
{
    //�� �̹��� ��ü�� ������ ������Ʈ
    GameObject All_Map;

    //�� �� �̹�����
    GameObject Map_Ice;
    GameObject Map_Pirates;
    GameObject Map_Forest;
    GameObject Map_Space;

    //���� ������ ��Ÿ�� �� �̹�����
    GameObject Blur_ice;
    GameObject Blur_Pirates;
    GameObject Blur_Forest;
    GameObject Blur_Space;

    //�� ���������� �˸��� �̹��� �ҷ������
    Image Fade_img;

    //�� ���� ���
    //video_play��� ��ü ���� ��ũ��Ʈ ���
    video_play ice_video;
    video_play sea_video;
    video_play forest_video;
    video_play space_video;

    //� ������ ������
    int Map_number = 0;
    //�¿� �̵� �����ϱ� ����
    int Map_select = 0;
    //�� �̵��� ������
    int Map_move = 250;
    //��,��� �̵���
    float Max_move = 0;
    //�����̰� �ִ��� Ȯ��
    int amimoveing = 0;
    //�� ���ÿϷ� Ȯ��
    bool map_select_done = false;
    //�� �̵��ӵ�
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

    //�ʼ���
    void Map_Select()
    {
        //1P�� ���� ������
        //�̹����� �����̵� ���·� �Ѿ�� ���� ���� �Ұ�(�̹����� ���� ���¿����� ��,�� �̵� ����)
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
        //�Է��� ������ ���߱� ����
        if (Input.GetAxisRaw("Horizontal_Dpad_X") == 0)
            joy1dpad = false;

        //�����̵�
        if (Map_select == 1)
        {
            //�����̵� �Է� Ȯ��
            if (All_Map.transform.position.x > -Max_move)
            {
                //�̵��ӵ� ����
                Map_move_speed = 1.2f;
                //�������� �̵�
                All_Map.transform.Translate(new Vector3(-Map_move, 0, 0) * Map_move_speed * Time.deltaTime);
            }
            else
            {
                //�Ϸ�Ǹ� �̵��ӵ� ����
                Map_move_speed = 0.0f;
                
                //�̵��Ϸ��� ��Ʋ�� ������
                if (All_Map.transform.position.x % 10 < 0)
                {                   
                    All_Map.transform.position = new Vector3(-Max_move, All_Map.transform.position.y, All_Map.transform.position.z);
                }
            }
        }
        //�����̵�
        if (Map_select == 2)
        {
            //�����̵� �Է� Ȯ��
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

        //1����
        if (Map_number == 0)
        {
            ice_video.go_play = true;
            sea_video.go_play = false;
            forest_video.go_play = false;
            space_video.go_play = false;
        }
        //2����
        if (Map_number == 1)
        {
            ice_video.go_play = false;
            sea_video.go_play = true;
            forest_video.go_play = false;
            space_video.go_play = false;
        }
        //3����
        if (Map_number == 2)
        {
            ice_video.go_play = false;
            sea_video.go_play = false;
            forest_video.go_play = true;
            space_video.go_play = false;
        }
        //4����
        if (Map_number == 3)
        {
            ice_video.go_play = false;
            sea_video.go_play = false;
            forest_video.go_play = false;
            space_video.go_play = true;
        }

        //�� ���ÿϷ�
        if ((Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            map_select_done = true;
        }

        //���� ���������� �ٷ� �Ѿ�� �ʰ� �ش��ϴ� �� �̹��� �ҷ������
        if (map_select_done)
        {
            if (Map_number == 0)
            {
                //�ش��ϴ� �� �̹����� �ҷ���
                Blur_ice.SetActive(true);
                Fade_img = Blur_ice.GetComponent<Image>();
                //�� �̹��� �ҷ��ö� Fade_In ȿ��
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

    //���ÿϷ�� �ҷ����� �Լ�
    void Map_Select_Done()
    {
        //�� ���¿��� �ѹ��� �Ϸ� ��ư�� ������ �ε������� �Ѿ
        if ((Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            SceneManager.LoadScene("Loading");
        }
        //�� ���¿��� ��� ��ư ������ �� �̹��� ���������� ���ư�
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            map_select_done = false;

            //��ȿ�� �ֱ����� ���� �ʱ�ȭ
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