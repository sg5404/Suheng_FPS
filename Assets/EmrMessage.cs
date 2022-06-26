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
            padeText.text = "����� ���� ������ �� ����,\n����� ������� ��Ź�� ����� ������ ���������ϴ�.\n�ϱ����� ������ �߸��Ǿ��־����ϴ�. \n���� ������ ������ ����, �Ϸ簡 ���� �� �����ϴ�.\n�̷ο��� ������������.";
        }
        else if(day != 5)
        {
            pade.gameObject.SetActive(false);
        }

        if(day == 6)
        {
            //��ü����
            SceneManager.LoadScene("Main");
        }

        if (isTime)
        {
            if (!isNight)
            {
                switch (day)
                {
                    case 1:
                        missionText.text = "���� �Ǹ� ���͵��� �����մϴ�\n�����ϴ� ���͵�� ���� ��Ƴ�������!";
                        changeTime = 20; //20
                        isTime = false;
                        break;
                    case 2:
                        missionText.text = "����� �� �ϱ⸦ �߰��߽��ϴ�.\n�� �ϱ��忡�� �̷��� �ⱸ�� ã�Ұ� ���� �������� �����̶�� �������ֽ��ϴ�.\n�� �ⱸ�� �㿡�� �����ٰ� �մϴ�.\n�̷η� ���� ��������� ǥ�õ� ���� ���󰡼���.\n������� ���� �ִ� ���� ������ ������";
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
                        emrText.text = "���ݿ��� ��Ƴ�������!";
                        changeTime = 180; //180
                        isTime = false;
                        break;
                    case 2:
                        changeTime = 10800;
                        load.SetActive(true);
                        isTime = false;
                        break;
                    case 4:
                        missionText.text = "�̷ο��� ������������!";
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
            padeText.text = "����� Ż��������, ū �λ��� �Ծ� ���� ���� ���մϴ�.\n����� ������ ���� ���� ���θ����� ���ư� �ϱ⸦ ���ϴ�.\n����� ���� ����� �̰��� Ż���ϱ� ����...";
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
            timeText.text = $"���� ���� �ٲ�� ���� {(int)changeTime}�� ���ҽ��ϴ�.";
        }
        else
        {
            timeText.text = $"����� �̼��� ���� ������ ���� ���� �ٲ��� �ʽ��ϴ�.";
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
                //���̵���
                //��ġ ����
            }
        }
    }
}
