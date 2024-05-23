using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private int sec = 0;
    private int min = 0;
    public TextMeshProUGUI timeFlowText;
    public int delta = 1;
    public Dictionary<GameManager.GameState, Color> timerColors = new() 
    {
        {GameManager.GameState.Pause, Color.white},
        {GameManager.GameState.Win, Color.black},
        {GameManager.GameState.Lose, Color.black}
    };

    private void Awake()
    {
        timeFlowText.gameObject.SetActive(false);
        delta = 0;
        StartCoroutine(TimeFlow());
    }

    public void ContinueTimer()
    {

        delta = 1;
        timeFlowText.gameObject.SetActive(false);
    } 

    public void StopTimer() 
    {
        
        delta = 0;
        timeFlowText.color = timerColors[GameManager.Instance.state];
        timeFlowText.gameObject.SetActive(true);
    } 

    public IEnumerator TimeFlow()
    {
        while (true)
        {
            if (sec == 59)
            {
                min++;
                sec = -1;
            }
            sec += delta;
            timeFlowText.text = min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }

   
}
