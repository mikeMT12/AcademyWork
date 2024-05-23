using System.Collections;
using UnityEngine;

public class WindObstacle : MonoBehaviour
{
    public int force = 100;
    public LayerMask windLayer;
    private Vector3 windDirect;
    public bool now = false;
    [SerializeField] GameObject Fan;
    [SerializeField] GameObject FanObj;
    [SerializeField] GameObject vizualFloor;
    float rotationSpeed = 30f;
    Quaternion newRotation;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            now = true;
            StartCoroutine(VizualPart());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "PlayerObject")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(windDirect*force, ForceMode.Force);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        now = false;
        StopCoroutine(VizualPart());  
    }


    private void Update()
    {
       
        if (now)
        {
            Fan.transform.rotation = Quaternion.RotateTowards(Fan.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            windDirect = FanObj.transform.forward;
        }
          
    }

    private IEnumerator VizualPart()
    {
        while (now)
        {
            Vector3 rotate = Fan.transform.eulerAngles;
            rotate.y = Random.Range(-180, 180); 
            newRotation = Quaternion.Euler(Fan.transform.localEulerAngles + rotate);
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(2);
    }


}
