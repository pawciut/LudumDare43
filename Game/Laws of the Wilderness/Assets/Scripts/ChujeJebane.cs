using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChujeJebane : MonoBehaviour
{
    public float speed = 20f;
    bool facingRight = true;
    float moveHorizontal = 0;
    private new Rigidbody2D rigidbody2D;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal") * speed;
        rigidbody2D.velocity = new Vector2(moveHorizontal, rigidbody2D.velocity.y);

        animator.SetBool("IsMoving", moveHorizontal != 0);

        Flip();
    }


    void Flip()
    {
        if ((moveHorizontal > 0 && !facingRight)
            ||
            (moveHorizontal < 0 && facingRight)
            )
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }
}