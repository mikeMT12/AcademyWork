using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int sec = 0;
    private int min = 0;
    private Text timeFlowText;
    public int delta = 1;

    
    void Start()
    {

        StartCoroutine(TimeFlow());


    }

    /*void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

            delta = 0;
            Debug.Log("Дошел до конца");
        }
    }*/
    IEnumerator TimeFlow()
    {
        while (true)
        {
            if (sec == 59)
            {
                min++;
                sec = -1;
            }
            sec += delta;
            //timeFlowText.text = min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }

   
}
