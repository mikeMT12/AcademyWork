using System.Collections;
using UnityEngine;

public class WindObstacleWithoutViz : MonoBehaviour
{

    public int force = 100;
    private Vector3 windDirect;
    public bool now = false;
    [SerializeField] GameObject vizualFloor;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            now = true;
            StartCoroutine(WindDirection());

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "PlayerObject")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(windDirect * force, ForceMode.Force);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        now = false;
        StopCoroutine(WindDirection());
    }


    private IEnumerator WindDirection()
    {
        while (now)
        {
            Vector3 randDirect = Random.insideUnitSphere;
            windDirect = new Vector3(randDirect.x, 0, randDirect.z);
            float angle = Vector3.Angle(vizualFloor.transform.right, windDirect);
            Vector3 rotate = vizualFloor.transform.eulerAngles;
            rotate.z= angle;
            vizualFloor.transform.rotation = Quaternion.Euler(rotate);
            yield return new WaitForSeconds(2);

        }

    }


}