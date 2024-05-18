using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObstacle : MonoBehaviour
{
    public int damage;
    public LayerMask layerDamage;
    private bool isTrapActive = false;
    private bool getInTrap = false;

    [SerializeField] private GameObject vizualObject;
    [SerializeField] private Material inActiveMaterial;
    [SerializeField] private Material ActiveMaterial;
    [SerializeField] private Material DamageMaterial;
    private Material nowMaterial;



    public bool CheckInTrap()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<CapsuleCollider>().radius * transform.localScale.x);

        //Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, GetComponent<BoxCollider>().size / 2, Quaternion.identity, layerDamage);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Player")
            {
                //print($"HIT TO DAMAGE Player - {hitColliders[i].gameObject.name}");
                getInTrap = true;
                break;
            }
        }
        return getInTrap;
    }


    void Start()
    {
        SetMaterial(inActiveMaterial);
    }

    public void DamagePlayer()
    {
        //SetMaterial(DamageMaterial);
        EventBus.TakeDamage.Invoke(damage);
        print("DAMAGE!");
    }

    public void SetMaterial(Material material)
    {
        nowMaterial = material;
        vizualObject.GetComponent<MeshRenderer>().material = nowMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(ActivateTrap());
        }
    }

    private IEnumerator ActivateTrap()
    {

        if (isTrapActive == false)
        {
            isTrapActive = true;

            //Debug.Log("SetTrapActive");

            SetMaterial(ActiveMaterial);
            //Debug.Log("You get in the TRAP! 1 second to damage!");

            yield return new WaitForSeconds(1.6f);
            CheckInTrap();
            
            if (CheckInTrap())
            {   
                DamagePlayer();
            }
            SetMaterial(DamageMaterial);
            getInTrap = false;
            
            yield return new WaitForSeconds(0.3f);

            SetMaterial(inActiveMaterial);

            yield return new WaitForSeconds(5);

            isTrapActive = false;

            if (CheckInTrap())
            {
                StartCoroutine(ActivateTrap());
            }

            
        }
       
    }

}
