using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindObstacleWithoutViz : MonoBehaviour
{

    public int force = 100;
    public LayerMask windLayer;
    private Vector3 windDirect;
    public bool now = false;
    public int rotationSpeed;
    private Quaternion winDirectQuaternion;
    [SerializeField] GameObject vizualFloor;

    void Start()
    {

    }


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
            winDirectQuaternion = Quaternion.Euler(windDirect.x, windDirect.y, windDirect.z);
            //float angle = Mathf.Atan2(vizualFloor.transform.position.y - transform.position.y, mousePos.x - person.x) * Mathf.Rad2Deg;
            float angle = Vector3.Angle(vizualFloor.transform.right, windDirect);
            Vector3 rotate = vizualFloor.transform.eulerAngles;
            rotate.z= angle;
            vizualFloor.transform.rotation = Quaternion.Euler(rotate);
        
            //StartCoroutine(RotateFloor());
            yield return new WaitForSeconds(2);

        }

        //StartCoroutine(WindDirection());
    }


}