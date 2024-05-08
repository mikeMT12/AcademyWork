using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObstacle : MonoBehaviour
{
    public int damage;
    public LayerMask layerDamage;
    private bool isTrapActive = false;
    //private EventBus eventBus;
    //[SerializeField] private GameObject triggerCollider;

    [SerializeField] private GameObject vizualObject;
    [SerializeField] private Material inActiveMaterial;
    [SerializeField] private Material ActiveMaterial;
    [SerializeField] private Material DamageMaterial;
    private Material nowMaterial;

    void Start()
    {
        SetMaterial(inActiveMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void Bind(EventBus EventBus)
    {
        eventBus = EventBus;
    }*/


    public void DamagePlayer()
    {
        //EventBus.Instance.TakeDamage?.Invoke(damage);
        //EventBus.TriggerTakeDamage(damage);
        
        SetMaterial(DamageMaterial);
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
            yield return new WaitForSeconds(1);
            CheckDamage();
            //DamagePlayer();
            yield return new WaitForSeconds(0.3f);
            SetMaterial(inActiveMaterial);
            yield return new WaitForSeconds(5);
            //Debug.Log("HaveWait5sec");
            isTrapActive = false;


        }
        //ChechOverlap(CheckDamage());

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<BoxCollider>().size.x);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            print(hitColliders[i].gameObject.name);
            if (hitColliders[i].gameObject.tag == "Player")
            {
                isTrapActive = false;
                StartCoroutine(ActivateTrap());
                break;
            }
        }
        
    }

    private void CheckDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4f);
        //Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, GetComponent<BoxCollider>().size / 2, Quaternion.identity, layerDamage);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            print(hitColliders[i].gameObject.name);
            if (hitColliders[i].gameObject.tag == "Player")
            {
                isTrapActive = false;
                DamagePlayer();
                break;  
            }
        }
        
    }

   /* private void ChechOverlap (var voidTodo)
    {

    }*/
}
