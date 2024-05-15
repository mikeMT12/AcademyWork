using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    int calls = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && calls == 0)
        {
            calls++;
            GameManager.Instance.UpdateGameState(GameManager.GameState.Win);
            
        }
    }

}