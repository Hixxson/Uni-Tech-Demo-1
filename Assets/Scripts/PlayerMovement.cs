using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance
    {
        set; get;
    }

    public float speed;
    public float jump;
    float moveVelocity;
    private Rigidbody2D rb;
    public Animator animator;
    public bool isGrounded;
    internal SpriteRenderer _renderer;

    public LayerMask groundLayer;

    private bool isJumping = false;

    public float disToGround = 0.7f;

    public float maxFuel = 5f;
    private float currentFuel = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentFuel = maxFuel;

        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }


        UIHandler.Instance.fuelSlider.minValue = 0;
        UIHandler.Instance.fuelSlider.maxValue = maxFuel;
    }

    private void Update()
    {

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, disToGround, groundLayer);

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _renderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _renderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        moveVelocity = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVelocity = -speed;
            animator.SetBool("isrunning", true);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVelocity = speed;
            animator.SetBool("isrunning", true);
        }
        else
        {
            animator.SetBool("isrunning", false);
        }

        Jetpack();

        ReplenishFuel();

        if (UIHandler.Instance.fuelSlider.value != currentFuel)
        {
            UIHandler.Instance.fuelSlider.value = currentFuel;
        }


        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
    }

    private void Jetpack()
    {
        if (Input.GetKey(KeyCode.Space) && !isGrounded && !isJumping && currentFuel > 0)
        {
            rb.AddForce(Vector2.up * 10, ForceMode2D.Force);

            currentFuel -= Time.deltaTime;

            if (currentFuel <= 0)
            {
                currentFuel = 0;
            }
        }
    }

    private void ReplenishFuel()
    {
        if (isGrounded && currentFuel < maxFuel)
        {
            currentFuel += Time.deltaTime;

            if (currentFuel >= maxFuel)
            {
                currentFuel = maxFuel;
            }
        }
    }
}
