using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Collider2D coll;
    private Animator anim;

    [SerializeField] private LayerMask ground;
    [SerializeField] private int CharacterSpeed = 6;
    [SerializeField] private float hurtForce = 6f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float health;
    [SerializeField] private Text healthAmount;

    private enum State { idle, running, jumping, attacking, hurt, falling}

    private State state = State.idle;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmount.text = health.ToString();
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
           rb2D.velocity = new Vector2(-CharacterSpeed, rb2D.velocity.y);
           transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0)
        {
            rb2D.velocity = new Vector2(CharacterSpeed, rb2D.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            state = State.jumping;
        }
        velocityState();
        anim.SetInteger("state", (int)state);
    }

    private void velocityState()
    {

        if(state == State.jumping)
        {
            if(rb2D.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if(Mathf.Abs(rb2D.velocity.x) > 1f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb2D.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb2D.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb2D.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (state == State.attacking)
            {
                Destroy(other.gameObject);
            }
            else
            {
                HandleHealth();
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb2D.velocity = new Vector2(-hurtForce, rb2D.velocity.y);
                }
                else
                {
                    rb2D.velocity = new Vector2(hurtForce, rb2D.velocity.y);
                }
            }
        }
    }

    private void HandleHealth()
    {
        state = State.hurt;
        health -= 0.5f;
        healthAmount.text = health.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
    }
}
