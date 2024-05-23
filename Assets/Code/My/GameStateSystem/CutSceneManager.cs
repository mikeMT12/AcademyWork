using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
    
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
        }  
    }


    public void Skip()
    {
        num += 1;
    }

    public void SetOverTrue()
    {
        over = true;
    }


}
