using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Slider healthSlider;

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health / 100f;
        
        if (health <= 0)
            Destroy(gameObject);
    }


    
    
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}