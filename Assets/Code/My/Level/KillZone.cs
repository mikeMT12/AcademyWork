using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "PlayerObject")
        {
			col.gameObject.GetComponent<PhysicsMovement>().LoadCheckPoint();
		}
        
        else if(col.gameObject.tag == "ball")
        {
            Destroy(col.gameObject);
        }
	}
}
