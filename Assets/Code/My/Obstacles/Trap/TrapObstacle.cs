using System.Collections;
using UnityEngine;

public class TrapObstacle : MonoBehaviour
{
    public int damage;
    public LayerMask layerDamage;
    private bool isTrapActive = false;
    private bool getInTrap = false;

    public float activeTime = 1.6f;
    public float damageTime = 0.3f;
    public float resetTime = 5;

    [SerializeField] private GameObject vizualObject;
    [SerializeField] private Material inActiveMaterial;
    [SerializeField] private Material ActiveMaterial;
    [SerializeField] private Material DamageMaterial;
    private Material nowMaterial;



    public bool CheckInTrap()
    {
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<CapsuleCollider>().radius * transform.localScale.x);

        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, GetComponent<BoxCollider>().size*2, gameObject.transform.rotation, layerDamage);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Player")
            {
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
            SetMaterial(ActiveMaterial);
            //Debug.Log("You get in the TRAP! 1 second to damage!");
            yield return new WaitForSeconds(activeTime);

            CheckInTrap();
            if (CheckInTrap())
            {   
                DamagePlayer();
            }
            SetMaterial(DamageMaterial);
            getInTrap = false;
            yield return new WaitForSeconds(damageTime);

            SetMaterial(inActiveMaterial);
            yield return new WaitForSeconds(resetTime);

            isTrapActive = false;
            if (CheckInTrap())
            {
                StartCoroutine(ActivateTrap());
            }
        }  
    }

}
