using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EmrMessage : MonoBehaviour
{
    [SerializeField]
    private Text emrText;
    [SerializeField]
    private Text missionText;

    [SerializeField]
    private Image pade;

    public Text timeText;

    [SerializeField]
    private Light sun;
    [SerializeField]
    private Light sun2;
    [SerializeField]
    private GameObject load;
    [SerializeField]
    private Text padeText;

    public int day = 0;
    public bool isNight = true;
    public bool isEnd = false;
    private bool isTime = true;
    private bool day2Clear = false;
    private bool padeout = false;
    Color color;
    private float changeTime = 0;
    float timer = 0;
    float timer2 = 0;
    private bool isOne = false;
    padeCtrl padectrl;

    // Start is called before the first frame update
    void Start()
    {
        day = 0;
        emrText.gameObject.SetActive(false);
        RenderSettings.fogDensity = 0;
        color = pade.color;
        padectrl = GetComponent<padeCtrl>();
        changeTime = 7;
        isTime = false;
        isNight = true;
        Screen.SetResolution(2560, 1440, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(day == 0)
        {
            pade.gameObject.SetActive(true);
        }
        else if(day == 3)
        {
            pade.gameObject.SetActive(true);
            padeText.text = "당신이 골인 지점에 들어간 순간,\n당신은 뒤통수에 둔탁한 충격을 느끼고 쓰러졌습니다.\n일기장의 정보는 잘못되어있었습니다. \n눈을 떠보니 깜깜한 저녘, 하루가 지난 것 같습니다.\n미로에서 빠져나가세요.";
        }
        else if(day != 5)
        {
            pade.gameObject.SetActive(false);
        }

        if(day == 6)
        {
            //씬체인지
            SceneManager.LoadScene("Main");
        }

        if (isTime)
        {
            if (!isNight)
            {
                switch (day)
                {
                    case 1:
                        missionText.text = "밤이 되면 몬스터들이 습격합니다\n습격하는 몬스터들로 부터 살아남으세요!";
                        changeTime = 20; //20
                        isTime = false;
                        break;
                    case 2:
                        missionText.text = "당신은 한 일기를 발견했습니다.\n그 일기장에는 미로의 출구를 찾았고 내일 빠져나갈 예정이라고 적혀져있습니다.\n그 출구는 밤에만 열린다고 합니다.\n미로로 가서 노란색으로 표시된 길을 따라가세요.\n노란길의 끝에 있는 골인 지점에 들어가세요";
                        changeTime = 20; //20
                        isTime = false;
                        break;
                    case 4:
                        break;

                    default:

                        break;
                }
            }
            else
            {
                switch(day)
                {
                    case 1:
                        emrText.text = "습격에서 살아남으세요!";
                        changeTime = 180; //180
                        isTime = false;
                        break;
                    case 2:
                        changeTime = 10800;
                        load.SetActive(true);
                        isTime = false;
                        break;
                    case 4:
                        missionText.text = "미로에서 빠져나가세요!";
                        changeTime = 10000;
                        isTime = false;
                        break;
                    case 5:
                        changeTime = 5;
                        isTime = false;
                        break;
                    default:
                        break;
                }
            }
        }

        if(isNight)
        {
            if(RenderSettings.fogDensity < 0.07f)
            {
                Fog(0.0007f);
            }
        }
        else
        {
            if (RenderSettings.fogDensity > 0)
            {
                Fog(-0.0014f);
            }
        }

        if(day == 4 && transform.position.x <= 200)
        {
            pade.gameObject.SetActive(true);
            day++;
            isNight = false;
            padeText.text = "당신은 탈출했으나, 큰 부상을 입어 오래 살지 못합니다.\n당신은 마지막 힘을 다해 오두막으로 돌아가 일기를 씁니다.\n당신의 다음 사람은 이곳을 탈출하길 빌며...";
            changeTime = 5;
            isTime = false;
        }


        if (changeTime <= 0 && !isTime)
        {
            if (isNight)
            {
                if(day != 3)
                {
                    isNight = false;
                    sun.gameObject.SetActive(true);
                    //sun2.gameObject.SetActive(true);
                }
                isTime = true;
                day++;
            }
            else
            {
                isNight = true;
                isTime = true;
                sun.gameObject.SetActive(false);
                //sun2.gameObject.SetActive(false);
                load.SetActive(false);
            }
        }

        changeTime -= Time.deltaTime;
        if(day < 2 || !isNight)
        {
            timeText.text = $"낯과 밤이 바뀌기 까지 {(int)changeTime}초 남았습니다.";
        }
        else
        {
            timeText.text = $"당신이 미션을 깨기 전까지 낯과 밤은 바뀌지 않습니다.";
        }

    }

    void Fog(float num)
    {
        timer += Time.deltaTime;
        if (timer > 0.1)
        {
            RenderSettings.fogDensity += num;
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isOne)
        {
            if (other.CompareTag("Clear"))
            {
                Debug.Log("b");
                day++;
                changeTime = 10;
                isOne = true;
                //padectrl.PadeIn();
                //페이드인
                //위치 변경
            }
        }
    }
}
