using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] List<PlayableDirector> cutScenes;
    [SerializeField] private Camera cutSceneCamera;

    public int num=0;
    public int x;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //num = 0; 
    }

    

    public void PlayCutScene()
    {
        Debug.Log($"катсцен - {cutScenes.Count}");
        print($"{num} - num"); 
        print($"{x} - x");
        cutSceneCamera.gameObject.SetActive(true);
        cutScenes[num].Play();
        

    }


    public void Skip()
    {
        cutSceneCamera.gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
