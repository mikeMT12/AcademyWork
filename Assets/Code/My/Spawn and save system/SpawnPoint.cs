using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Player")]
    //[SerializeField] private GameObject player;

    [Header("Choose option")]
    public bool setActive = false;
    void Awake()
    {

        //SpawnPlayer();
    }

    public void SpawnPlayer(GameObject player)
    {
        if (setActive)
        {
            player.transform.position = transform.position;
            player.SetActive(true);
        }
        else
        {
            Instantiate(player, transform.position, transform.rotation);
        }
    }

}
