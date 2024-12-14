using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float healthAmount = 100f;
    [SerializeField] float takeDamage;

    [SerializeField] bool isDead = false;

    [SerializeField] Image healthBar;
    void Start()
    {
        
    }

    void Update()
    {
        if (!isDead)
        {
            HorizontalMove();
        }
        else
        {
            Respawn();
        }
    }

    void HorizontalMove()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }

        dir.Normalize();

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }

    void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    void fillHealth()
    {
        healthBar.fillAmount = 100f;
        healthAmount = 100f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            TakeDamage(15);

            if(healthBar.fillAmount <= 0f)
            {
                isDead = true;
            }
        }
    }

    void Respawn()
    {
        isDead = false;

        transform.position = new Vector3(-0.75f, 1.12f, 0f);

        healthAmount = 100f;
        healthBar.fillAmount = 100f;
    }
}
