using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
    
public class CutSceneManager : MonoBehaviour
{
    [SerializeField] List<PlayableDirector> cutScenes;
    [SerializeField] private Camera cutSceneCamera;

    public static int num=0;
    public bool over = false;

    public void PlayCutScene()
    {
        
        
        if(num < cutScenes.Count)
        {
            cutSceneCamera.gameObject.SetActive(true);
            cutScenes[num].Play();
        }
        else
        {
            over = true;
            //cutScenes[0].Play();
            //GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
        }
       
    }


    public void Skip()
    {
        num += 1;
    }


}
