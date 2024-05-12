using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObstacle : MonoBehaviour
{
    public int damage;
    public LayerMask layerDamage;
    private bool isTrapActive = false;
    private bool isDamaged = false;
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

    public void DamagePlayer()
    {
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
        isDamaged = false;
        if (isTrapActive == false)
        {
            isTrapActive = true;
            //Debug.Log("SetTrapActive");
            SetMaterial(ActiveMaterial);
            //Debug.Log("You get in the TRAP! 1 second to damage!");
            yield return new WaitForSeconds(2);
            CheckDamage();
            if (!isDamaged)
            {
                SetMaterial(DamageMaterial);
            }
            //DamagePlayer();
            yield return new WaitForSeconds(0.3f);
            SetMaterial(inActiveMaterial);
            Debug.Log("HaveWait5sec");
            yield return new WaitForSeconds(5);
            isTrapActive = false;
            Debug.Log("WAITED");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<CapsuleCollider>().radius * transform.localScale.x);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                print($"HIT STAING - {hitColliders[i].gameObject.name}");
                if (hitColliders[i].gameObject.tag == "Player")
                {
                    print($"HIT STAING Player - {hitColliders[i].gameObject.name}");
                    StartCoroutine(ActivateTrap());
                    break;
                }
            }
        }
        //ChechOverlap(CheckDamage())
    }
    private void Update()
    {
        //Debug.DrawRay(transform.position, transform.right * GetComponent<CapsuleCollider>().radius * transform.localScale.x,Color.black);
    }

    private void CheckDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<CapsuleCollider>().radius * transform.localScale.x);
       
        //Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, GetComponent<BoxCollider>().size / 2, Quaternion.identity, layerDamage);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            print($"HIT TO DAMAGE - {hitColliders[i].gameObject.name}");
            if (hitColliders[i].gameObject.tag == "Player")
            {
                print($"HIT TO DAMAGE Player - {hitColliders[i].gameObject.name}");
                //isTrapActive = false;
                DamagePlayer();
                isDamaged = true;
                break;  
            }
        }
    }

   /* private void ChechOverlap (var voidTodo)
    {

    }*/
}
