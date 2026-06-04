using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Slider healthSlider;

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health / 100f;
        
        if (health <= 0)
            Destroy(gameObject);
    }
}
