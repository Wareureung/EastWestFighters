using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class new_Title : MonoBehaviour
{
    //�� �÷��̾���� ĳ���� ���� �̹���
    GameObject Player_select_arrow_1P;
    GameObject Player_select_arrow_2P;
    GameObject Player_select_arrow_3P;
    GameObject Player_select_arrow_4P;

    //�� �÷��̾���� ĳ���� ���� ��ȣ
    int Player1_select = 0;
    int Player2_select = 0;
    int Player3_select = 0;
    int Player4_select = 0;

    //�� �÷��̾���� ĳ���� ���� ����
    bool P1_select_done = false;
    bool P2_select_done = false;
    bool P3_select_done = false;
    bool P4_select_done = false;

    //ĳ���� ���п�
    bool ch1_state = false;
    bool ch2_state = false;
    bool ch3_state = false;
    bool ch4_state = false;

    

    void Start()
    {
        //�±׸� ����Ͽ� ������Ʈ Ȯ��
        Player_select_arrow_1P = GameObject.FindWithTag("Player_Select_Arrow_1P");
        Player_select_arrow_2P = GameObject.FindWithTag("Player_Select_Arrow_2P");
        Player_select_arrow_3P = GameObject.FindWithTag("Player_Select_Arrow_3P");
        Player_select_arrow_4P = GameObject.FindWithTag("Player_Select_Arrow_4P");
    }

    void Update()
    {
        Character_Select();
    }

    //ĳ���� ����
    void Character_Select()
    {
        //ĳ���� ���� ������ �Դ��� Ȯ��
        if (Player_Select_Mode)
        {
            //ĳ���Ͱ� ���ù�ư ��������(���� �Ϸ����� Ȯ��)
            if ((Input.GetKeyDown(KeyCode.Joystick1Button0)) && P1_select_done == false)
            {
                //1P
                //��ȣ�� Ȯ���ϰ� �ٸ� �÷��̾ �������� �ʾҴ��� Ȯ��
                if (Player1_select == 0 && !ch1_state)
                {
                    ch1_state = true;
                    P1_select_done = true;

                    //���ÿϷ�Ǹ� ���ÿϷ� ���ҽ��� ����
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

            //ĳ���� ������ �ϱ� ���� ��,��� �����̴� �κ�
            //������ �Ϸ���� �ʾƾ� �����̱� ������
            //��,�� �������� �����е� DPad ���
            //1P
            if (!P1_select_done)
            {
                //��,�� Ű �Է� �κ�
                //�����̵�
                if ((Input.GetAxisRaw("Horizontal_Dpad_X") == 1) && Player1_select < 3 && !joy1dpad)
                {
                    joy1dpad = true;
                    Player1_select++;
                    //�̹��� �̵��κ�
                    Player_select_arrow_1P.transform.Translate(new Vector2(50, 0));
                }
                //�����̵�
                if ((Input.GetAxisRaw("Horizontal_Dpad_X") == -1) && Player1_select > 0 && !joy1dpad)
                {
                    joy1dpad = true;
                    Player1_select--;
                    Player_select_arrow_1P.transform.Translate(new Vector2(-50, 0));
                }
                //������ ������ ������ �ֱ� ����
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
        //��� ĳ���� ������ �Ϸ�Ǹ� ���� ������ �Ѿ�� �κ�
        if (P1_select_done && P2_select_done && P3_select_done && P4_select_done)
        {
            Go_To_Next_Scene();
        }
    }
}