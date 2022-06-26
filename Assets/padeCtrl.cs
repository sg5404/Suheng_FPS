using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class padeCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Image pade;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = pade.color;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PadeIn()
    {
        Debug.Log("a");
        for (float i = 0; i < 1;)
        {
            i += 0.01f;
            color.a = i;
            pade.color = color;
            StartCoroutine(wait());
        }
        target.transform.position = new Vector3(76, 1.6f, 0);
        for (float i = 1; i > 0;)
        {
            i -= 0.01f;
            color.a = i;
            pade.color = color;
            StartCoroutine(wait());
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.02f);
    }
}
