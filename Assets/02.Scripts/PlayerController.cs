using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 80.0f;
    float mouseX;

    private Transform playerTransform;
    private Animation playerAnim;

    // �ʱ� ���� ��
    private readonly float initHp = 100.0f;
    // ���� ���� ��
    public float currHp;

    // Hpbar �̹��� ����
    private Image hpBar;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        playerTransform = GetComponent<Transform>();
        playerAnim = GetComponent<Animation>();

        playerAnim.Play("Idle");
        yield return new WaitForSeconds(0.3f);
        // hp �ʱ�ȭ
        currHp = initHp;
        // hpbar �̹��� ����
        hpBar = GameObject.FindGameObjectWithTag("HPBAR")?.GetComponent<Image>();
        // �ʱ�ȭ �� HP ǥ��
        DisplayHP();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r= Input.GetAxisRaw("Mouse X");


        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir.Normalize();

        mouseX += r * rotationSpeed * Time.deltaTime;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, mouseX, 0);

        PlayerAnimation(h, v);
    }

    void PlayerAnimation(float h, float v)
    {
        if (h <= -0.1f)
            playerAnim.CrossFade("RunL", 0.25f);
        else if (h >= 0.1f)
            playerAnim.CrossFade("RunR", 0.25f);
        else if (v <= -0.1f)
            playerAnim.CrossFade("RunB", 0.25f);
        else if (v >= 0.1f)
            playerAnim.CrossFade("RunF", 0.25f);
        else
            playerAnim.CrossFade("Idle", 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("PUNCH") && currHp >= 0.0f)
        {
            currHp -= 10.0f;
            Debug.Log($"Player HP = {currHp}");

            // HP ǥ��
            DisplayHP();

            if ( currHp <= 0.0f )
            {
                PlayerDie();
                SceneManager.LoadScene("GameOver");//GameOver
            }
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Die!");

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        foreach(GameObject monster in monsters )
        {
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }
        // ���� ����
        GameMgr.GetInstance().IsGameOver = true;

        //�� ü����
    }

    void DisplayHP()
    {
        hpBar.fillAmount = currHp / initHp;
    }
}