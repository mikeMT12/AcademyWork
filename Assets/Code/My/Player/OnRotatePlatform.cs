using UnityEngine;

public class OnRotatePlatform : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            
            this.transform.parent = collision.transform;
            gameObject.transform.localScale = new Vector3(1 / collision.transform.localScale.x, 1 / collision.transform.localScale.y, 1 / collision.transform.localScale.z);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            this.transform.parent = player.transform;
    }
}
