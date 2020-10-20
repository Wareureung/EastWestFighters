using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //숫자 이미지 위치
    Image min_one;
    Image min_two;
    Image sec_one;
    Image sec_two;

    //숫자 이미지 저장
    Sprite[] Number = new Sprite[10];
    
    //시간 계산
    float mytime;

    //시간 쪼개기
    int min_front = 0;   //십분
    int min_back = 0;    //분
    int sec_front = 0;   //십초
    int sec_back = 0;    //초

    //게임 시작되면 타이머 시작하기 위함
    canvas_script Cavas_DM;

    void Start()
    {
        min_one = GameObject.Find("min_00").GetComponent<Image>();
        min_two = GameObject.Find("min_01").GetComponent<Image>();
        sec_one = GameObject.Find("sec_00").GetComponent<Image>();
        sec_two = GameObject.Find("sec_01").GetComponent<Image>();

        Number[0] = Resources.Load<Sprite>("0");
        Number[1] = Resources.Load<Sprite>("1");
        Number[2] = Resources.Load<Sprite>("2");
        Number[3] = Resources.Load<Sprite>("3");
        Number[4] = Resources.Load<Sprite>("4");
        Number[5] = Resources.Load<Sprite>("5");
        Number[6] = Resources.Load<Sprite>("6");
        Number[7] = Resources.Load<Sprite>("7");
        Number[8] = Resources.Load<Sprite>("8");
        Number[9] = Resources.Load<Sprite>("9");

        Cavas_DM = GameObject.FindWithTag("canvas_DM").GetComponent<canvas_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cavas_DM.doplay && !Cavas_DM.stop_map)
        {
            mytime += Time.deltaTime;

            if (mytime > 1f)
            {
                if (sec_back == 9)
                {
                    //60초 단위로 끊기 위함
                    if (sec_front == 5)
                    {
                        min_back++;
                        sec_front = 0;
                        sec_back = 0;
                    }
                    else if (sec_front != 5)
                    {
                        sec_front++;
                        sec_back = 0;
                    }
                }
                else
                {
                    sec_back++;
                }
                min_one.sprite = Number[min_front];
                min_two.sprite = Number[min_back];
                sec_one.sprite = Number[sec_front];
                sec_two.sprite = Number[sec_back];
                mytime = 0.0f;
            }
        }
    }
}
