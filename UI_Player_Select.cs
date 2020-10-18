using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class new_Title : MonoBehaviour
{
    //각 플레이어들의 캐릭터 선택 이미지
    GameObject Player_select_arrow_1P;
    GameObject Player_select_arrow_2P;
    GameObject Player_select_arrow_3P;
    GameObject Player_select_arrow_4P;

    //각 플레이어들의 캐릭터 선택 번호
    int Player1_select = 0;
    int Player2_select = 0;
    int Player3_select = 0;
    int Player4_select = 0;

    //각 플레이어들의 캐릭터 선택 상태
    bool P1_select_done = false;
    bool P2_select_done = false;
    bool P3_select_done = false;
    bool P4_select_done = false;

    //캐릭터 구분용
    bool ch1_state = false;
    bool ch2_state = false;
    bool ch3_state = false;
    bool ch4_state = false;

    

    void Start()
    {
        //태그를 사용하여 오브젝트 확인
        Player_select_arrow_1P = GameObject.FindWithTag("Player_Select_Arrow_1P");
        Player_select_arrow_2P = GameObject.FindWithTag("Player_Select_Arrow_2P");
        Player_select_arrow_3P = GameObject.FindWithTag("Player_Select_Arrow_3P");
        Player_select_arrow_4P = GameObject.FindWithTag("Player_Select_Arrow_4P");
    }

    void Update()
    {
        Character_Select();
    }

    //캐릭터 선택
    void Character_Select()
    {
        //캐릭터 선택 순번이 왔는지 확인
        if (Player_Select_Mode)
        {
            //캐릭터가 선택버튼 눌렀을때(선택 완료됬는지 확인)
            if ((Input.GetKeyDown(KeyCode.Joystick1Button0)) && P1_select_done == false)
            {
                //1P
                //번호를 확인하고 다른 플레이어가 선택하지 않았는지 확인
                if (Player1_select == 0 && !ch1_state)
                {
                    ch1_state = true;
                    P1_select_done = true;

                    //선택완료되면 선택완료 리소스로 변경
                    Player_select_arrow_1P.GetComponent<Image>().sprite = Resources.Load("textures/1_red", typeof(Sprite)) as Sprite;
                }
                else if (Player1_select == 1 && !ch2_state)
                {
                    ch2_state = true;
                    P1_select_done = true;
                    Player_select_arrow_1P.GetComponent<Image>().sprite = Resources.Load("textures/1_yellow", typeof(Sprite)) as Sprite;
                }
                else if (Player1_select == 2 && !ch3_state)
                {
                    ch3_state = true;
                    P1_select_done = true;
                    Player_select_arrow_1P.GetComponent<Image>().sprite = Resources.Load("textures/1_green", typeof(Sprite)) as Sprite;
                }
                else if (Player1_select == 3 && !ch4_state)
                {
                    ch4_state = true;
                    P1_select_done = true;
                    Player_select_arrow_1P.GetComponent<Image>().sprite = Resources.Load("textures/1_blue", typeof(Sprite)) as Sprite;
                }
            }
            //2P
            if ((Input.GetKeyDown(KeyCode.Joystick2Button0)) && P2_select_done == false)
            {
                if (Player2_select == 0 && !ch1_state)
                {
                    ch1_state = true;
                    P2_select_done = true;
                    Player_select_arrow_2P.GetComponent<Image>().sprite = Resources.Load("textures/2_red", typeof(Sprite)) as Sprite;
                }
                else if (Player2_select == 1 && !ch2_state)
                {
                    ch2_state = true;
                    P2_select_done = true;
                    Player_select_arrow_2P.GetComponent<Image>().sprite = Resources.Load("textures/2_yellow", typeof(Sprite)) as Sprite;
                }
                else if (Player2_select == 2 && !ch3_state)
                {
                    ch3_state = true;
                    P2_select_done = true;
                    Player_select_arrow_2P.GetComponent<Image>().sprite = Resources.Load("textures/2_green", typeof(Sprite)) as Sprite;
                }
                else if (Player2_select == 3 && !ch4_state)
                {
                    ch4_state = true;
                    P2_select_done = true;
                    Player_select_arrow_2P.GetComponent<Image>().sprite = Resources.Load("textures/2_blue", typeof(Sprite)) as Sprite;
                }
            }
            //3P
            if ((Input.GetKeyDown(KeyCode.Joystick3Button0)) && P3_select_done == false)
            {
                if (Player3_select == 0 && !ch1_state)
                {
                    ch1_state = true;
                    P3_select_done = true;
                    Player_select_arrow_3P.GetComponent<Image>().sprite = Resources.Load("textures/3_red", typeof(Sprite)) as Sprite;
                }
                else if (Player3_select == 1 && !ch2_state)
                {
                    ch2_state = true;
                    P3_select_done = true;
                    Player_select_arrow_3P.GetComponent<Image>().sprite = Resources.Load("textures/3_yellow", typeof(Sprite)) as Sprite;
                }
                else if (Player3_select == 2 && !ch3_state)
                {
                    ch3_state = true;
                    P3_select_done = true;
                    Player_select_arrow_3P.GetComponent<Image>().sprite = Resources.Load("textures/3_green", typeof(Sprite)) as Sprite;
                }
                else if (Player3_select == 3 && !ch4_state)
                {
                    ch4_state = true;
                    P3_select_done = true;
                    Player_select_arrow_3P.GetComponent<Image>().sprite = Resources.Load("textures/3_blue", typeof(Sprite)) as Sprite;
                }
            }
            //4P
            if ((Input.GetKeyDown(KeyCode.Joystick4Button0)) && P4_select_done == false)
            {
                if (Player4_select == 0 && !ch1_state)
                {
                    ch1_state = true;
                    P4_select_done = true;
                    Player_select_arrow_4P.GetComponent<Image>().sprite = Resources.Load("textures/4_red", typeof(Sprite)) as Sprite;
                }
                else if (Player4_select == 1 && !ch2_state)
                {
                    ch2_state = true;
                    P4_select_done = true;
                    Player_select_arrow_4P.GetComponent<Image>().sprite = Resources.Load("textures/4_yellow", typeof(Sprite)) as Sprite;
                }
                else if (Player4_select == 2 && !ch3_state)
                {
                    ch3_state = true;
                    P4_select_done = true;
                    Player_select_arrow_4P.GetComponent<Image>().sprite = Resources.Load("textures/4_green", typeof(Sprite)) as Sprite;
                }
                else if (Player4_select == 3 && !ch4_state)
                {
                    ch4_state = true;
                    P4_select_done = true;
                    Player_select_arrow_4P.GetComponent<Image>().sprite = Resources.Load("textures/4_blue", typeof(Sprite)) as Sprite;
                }
            }

            //캐릭터 선택을 하기 위해 좌,우로 움직이는 부분
            //선택이 완료되지 않아아 움직이기 가능함
            //좌,우 움직임은 게임패드 DPad 사용
            //1P
            if (!P1_select_done)
            {
                //좌,우 키 입력 부분
                //우측이동
                if ((Input.GetAxisRaw("Horizontal_Dpad_X") == 1) && Player1_select < 3 && !joy1dpad)
                {
                    joy1dpad = true;
                    Player1_select++;
                    //이미지 이동부분
                    Player_select_arrow_1P.transform.Translate(new Vector2(50, 0));
                }
                //좌측이동
                if ((Input.GetAxisRaw("Horizontal_Dpad_X") == -1) && Player1_select > 0 && !joy1dpad)
                {
                    joy1dpad = true;
                    Player1_select--;
                    Player_select_arrow_1P.transform.Translate(new Vector2(-50, 0));
                }
                //누르지 않으면 가만히 있기 위함
                if (Input.GetAxisRaw("Horizontal_Dpad_X") == 0)
                    joy1dpad = false;
            }
            //2P
            if (!P2_select_done)
            {
                if ((Input.GetAxisRaw("Horizontal1_Dpad_X") == 1) && Player2_select < 3 && !joy2dpad)
                {
                    joy2dpad = true;
                    Player2_select++;
                    Player_select_arrow_2P.transform.Translate(new Vector2(50, 0));
                }
                if ((Input.GetAxisRaw("Horizontal1_Dpad_X") == -1) && Player2_select > 0 && !joy2dpad)
                {
                    joy2dpad = true;
                    Player2_select--;
                    Player_select_arrow_2P.transform.Translate(new Vector2(-50, 0));
                }
                if (Input.GetAxisRaw("Horizontal1_Dpad_X") == 0)
                    joy2dpad = false;
            }
            //3P
            if (!P3_select_done)
            {
                if ((Input.GetAxisRaw("Horizontal2_Dpad_X") == 1) && Player3_select < 3 && !joy3dpad)
                {
                    joy3dpad = true;
                    Player3_select++;
                    Player_select_arrow_3P.transform.Translate(new Vector2(50, 0));
                }
                if ((Input.GetAxisRaw("Horizontal2_Dpad_X") == -1) && Player3_select > 0 && !joy3dpad)
                {
                    joy3dpad = true;
                    Player3_select--;
                    Player_select_arrow_3P.transform.Translate(new Vector2(-50, 0));
                }
                if (Input.GetAxisRaw("Horizontal2_Dpad_X") == 0)
                    joy3dpad = false;
            }
            //4P
            if (!P4_select_done)
            {
                if ((Input.GetAxisRaw("Horizontal3_Dpad_X") == 1) && Player4_select < 3 && !joy4dpad)
                {
                    joy4dpad = true;
                    Player4_select++;
                    Player_select_arrow_4P.transform.Translate(new Vector2(50, 0));
                }
                if ((Input.GetAxisRaw("Horizontal3_Dpad_X") == -1) && Player4_select > 0 && !joy4dpad)
                {
                    joy4dpad = true;
                    Player4_select--;
                    Player_select_arrow_4P.transform.Translate(new Vector2(-50, 0));
                }
                if (Input.GetAxisRaw("Horizontal3_Dpad_X") == 0)
                    joy4dpad = false;
            }
        }
        //모든 캐릭터 선택이 완료되면 다음 씬으로 넘어가는 부분
        if (P1_select_done && P2_select_done && P3_select_done && P4_select_done)
        {
            Go_To_Next_Scene();
        }
    }
}