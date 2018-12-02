using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterPlayerController : MonoBehaviour
{
    /// <summary>
    /// przy tym poziomie nierownosci nie traktujemy tego jako skakanie
    /// </summary>
    float terrainTreshold = 0.13f;

    public float speed = 20f;
    bool facingRight = true;
    float moveHorizontal = 0;
    bool grounded = false;
    public Transform[] groundCheck;
    float groundRadius = 0.2f;
    public LayerMask groundLayer;

    bool triggerJump = false;
    public float jumpPower = 5f;
    public float fallMultiplayer = 2.5f;
    public float lowJumpMultiplier = 2f;

    Input2 Input2;


    private new Rigidbody2D rigidbody2D;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Input2 = new Input2();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    void FixedUpdate()
    {
        IsGrounded();
    }

    void IsGrounded()
    {
        if(rigidbody2D.velocity.y <= 0)
        {
            foreach (var gc in groundCheck)
            {
                var colliders = Physics2D.OverlapCircleAll(gc.position, groundRadius, groundLayer);
                for(int i=0;i<colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        animator.SetBool("IsGrounded", grounded = true);
                        return;
                    }
                }
            }
        }
        animator.SetBool("IsGrounded", grounded = false);

    }

    void Move()
    {

        moveHorizontal = Input2.GetHorizontal() * speed;



        //Input2.GetJumpDown(() =>
        //{
        //    Debug.Log("Jumping");
        //    if(grounded)
        //        triggerJump = true;
        //});

        animator.SetBool("IsMoving", moveHorizontal != 0);


        Vector2 jumpVector = Vector2.zero;
        if (grounded && Input.GetButtonDown("Jump"))
        {
            grounded = false;

            //rigidbody2D.AddForce(Vector2.up * jumpPower);
            jumpVector = Vector2.up * jumpPower;

            Debug.Log($"Force {Vector2.up * jumpPower}");
            triggerJump = false;
        }
        if (rigidbody2D.velocity.y < 0)
        {
            jumpVector += Vector2.up * Physics2D.gravity.y * (fallMultiplayer - 1) * Time.deltaTime;
        }
        else if (rigidbody2D.velocity.y > 0 && !Input2.GetJumpButton())
            jumpVector += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        rigidbody2D.velocity = new Vector2(moveHorizontal, rigidbody2D.velocity.y) + jumpVector;




        Flip();
    }

    void Attack()
    {
        if(Input.GetButton("Stab"))
        {
            animator.SetBool("Attack_Stab", true);
        }
        else
            animator.SetBool("Attack_Stab", false);

        if (Input.GetButton("Throw"))
            animator.SetBool("Attack_Throw", true);
        else
            animator.SetBool("Attack_Throw", false);
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