using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToChase = 10f;
    [SerializeField] private Transform Target;

    private Rigidbody rb;

    
    
    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float distanceToTarget = Vector3.Distance(transform.position, Target.position);
        
        if (distanceToTarget <= distanceToChase)
        {
            Vector3 direction = (Target.position - transform.position).normalized;
            
            rb.MovePosition(transform.position + direction * speed / 100f);
        }
    }

    
    
    
    

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