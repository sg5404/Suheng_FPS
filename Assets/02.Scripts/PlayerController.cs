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

    // 초기 생명 값
    private readonly float initHp = 100.0f;
    // 현재 생명 값
    public float currHp;

    // Hpbar 이미지 연결
    private Image hpBar;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        playerTransform = GetComponent<Transform>();
        playerAnim = GetComponent<Animation>();

        playerAnim.Play("Idle");
        yield return new WaitForSeconds(0.3f);
        // hp 초기화
        currHp = initHp;
        // hpbar 이미지 연결
        hpBar = GameObject.FindGameObjectWithTag("HPBAR")?.GetComponent<Image>();
        // 초기화 된 HP 표시
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
        if(other.CompareTag("Potion"))
        {
            moveSpeed += 1;
            other.gameObject.SetActive(false);
        }
        if( other.CompareTag("PUNCH") && currHp >= 0.0f)
        {
            currHp -= 10.0f;
            Debug.Log($"Player HP = {currHp}");

            // HP 표시
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
        // 게임 종료
        GameMgr.GetInstance().IsGameOver = true;

        //씬 체인지
    }

    void DisplayHP()
    {
        hpBar.fillAmount = currHp / initHp;
    }
}
