using UnityEngine;

public class Shooter : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    [SerializeField, Range(0, 10000)] private float _force;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;   
    }

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();

                if (damageable != null)
                {
                    Vector3 forceDirection = (hit.point - _camera.transform.position).normalized;
                    forceDirection.y = 0;

                    damageable.TakeDamage(forceDirection * _force, hit.point);
                }
            }
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();
        //Debug.Log(tag);
        if (damageable != null && collision.gameObject.tag == "Player")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.Log("¡≈…!");
                Debug.DrawRay(contact.point, contact.normal, Color.white);
                Vector3 forceDirection = (collision.gameObject.transform.position - contact.point ).normalized;
                forceDirection.y = 0;

                damageable.TakeDamage(forceDirection * _force, (contact.point));
            }
           
        }
    }
}
