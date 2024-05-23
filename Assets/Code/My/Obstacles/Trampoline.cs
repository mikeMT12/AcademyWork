using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public int force;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "PlayerObject")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
