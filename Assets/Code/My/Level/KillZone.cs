using UnityEngine;
using Cinemachine;

public class KillZone : MonoBehaviour
{
    public bool killPlayer = true;
    [SerializeField] private Camera mainCamera;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "PlayerObject")
        {
            if (killPlayer)
            {
                col.gameObject.GetComponent<HealthSystem>().Damage(col.gameObject.GetComponent<HealthSystem>().maxHealth);
                mainCamera.GetComponent<CinemachineBrain>().enabled = false;
            }
            else
                col.gameObject.GetComponent<PhysicsMovement>().LoadCheckPoint();

        }
        
        else if(col.gameObject.tag == "ball")
        {
            Destroy(col.gameObject);
        }
	}
}
