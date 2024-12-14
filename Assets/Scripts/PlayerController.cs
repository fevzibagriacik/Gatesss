using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    //[SerializeField] private float healthAmount = 100f;
    //[SerializeField] private float takeDamage;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float wallSlidingSpeed = 2f;
    [SerializeField] private float wallJumpingTime = 0.2f;
    [SerializeField] private float wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration = 0.4f;

    [SerializeField] private int currentHealthIndex = 4;

    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isGround;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isWallJumping = false;
    [SerializeField] private bool atTarget = false;

    //[SerializeField] private Image healthBar;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 16f);
    [SerializeField] private Vector3 spawnPoint;

    public Animator targetAnimator;

    public List<Image> healths;

    public Sprite fullHealth;
    public Sprite emptyHealth;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (!isDead)
        {
            //HorizontalMove();
        }
        else if(isDead)
        {
            Respawn();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDead && isGround)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        WallSlide();

        WallJump();
    }

    private void FixedUpdate()
    {
        if (!isWallJumping && !isDead)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        }
    }

    /*void HorizontalMove()
    {
        isMoving = false;
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            dir.x = 1;
        }

        dir.Normalize();

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }*/

    /*void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;


    }*/

    /*void fillHealth()
    {
        healthBar.fillAmount = 100f;
        healthAmount = 100f;
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            /*TakeDamage();

            if(healthBar.fillAmount <= 0f)
            {
                isDead = true;
            }*/
            healths[currentHealthIndex].sprite = emptyHealth;
            currentHealthIndex--;

            if(currentHealthIndex == -1)
            {
                isDead = true;
                Respawn();
            }
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            isDead = true;
            Respawn();
        }

        if (collision.gameObject.CompareTag("Target"))
        {
            targetAnimator.SetBool("atTarget", true);
        }
        else
        {
            targetAnimator.SetBool("atTarget", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gate1"))
        {
            transform.position = new Vector3(-7.53999996f, 5.5999999f, 0);
        }
    }

    void Respawn()
    {
        currentHealthIndex = 4;

        isDead = false;

        transform.position = spawnPoint;

        for(int i = 0; i < healths.Count; i++)
        {
            healths[i].sprite = fullHealth;
        }

        /*healthAmount = 100f;
        healthBar.fillAmount = 100f;*/
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    void WallSlide()
    {
        if (IsWalled() && !isGround && isMoving)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
        }

        Invoke(nameof(StopWallJumping), wallJumpingDuration);
    }

    void StopWallJumping()
    {
        isWallJumping = false;
    }
}
