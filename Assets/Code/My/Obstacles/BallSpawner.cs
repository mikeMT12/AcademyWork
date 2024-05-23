using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;
    public int force;

    public float minDeltaTime;
    public float maxDeltaTime;

    void Start()
    {     
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        while (true)
        {
            if(GameManager.Instance.state == GameManager.GameState.Game)
            {
                GameObject ball = Instantiate(ballPrefab, spawnPoint); ;
                ball.GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Force);
            }

            yield return new WaitForSeconds(Random.Range(minDeltaTime, maxDeltaTime));
        }
    }


}
