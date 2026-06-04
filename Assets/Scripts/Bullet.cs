using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;
    [SerializeField] private int damage = 10;
    
    
    
    private void Start()
    {
        rb.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
        
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        
        Destroy(gameObject);
    }
}
