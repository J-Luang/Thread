using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float leftWayPoint;
    [SerializeField] private float rightWayPoint;

    [SerializeField] private float jumpDistance = 5;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;
    private Rigidbody2D rb;
    private AudioSource slimeJumpSound;

    private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        slimeJumpSound = GetComponent<AudioSource>();
    }

    private void CheckRoatation()
    {
        if (transform.rotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.identity;
        }
    }
    private void SlimeJump()
    {
        slimeJumpSound.Play();
    }

    private void EnemyMovement()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftWayPoint)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    SlimeJump();
                    rb.velocity = new Vector2(-jumpDistance, jumpHeight);
                }
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {
            if (transform.position.x < rightWayPoint)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    SlimeJump();
                    rb.velocity = new Vector2(jumpDistance, jumpHeight);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    private void Update()
    {
        CheckRoatation();
        EnemyMovement();
    }
}
