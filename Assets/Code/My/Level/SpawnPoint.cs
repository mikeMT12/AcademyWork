using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Choose option")]
    public bool setActive = false;


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
