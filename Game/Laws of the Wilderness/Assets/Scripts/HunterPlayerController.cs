using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterPlayerController : MonoBehaviour
{
    //HorizontalMovement
    public float speed = 20f;
    bool facingRight = true;
    float moveHorizontal = 0;

    //Ground check
    bool grounded = false;
    public Transform[] groundCheck;
    float groundRadius = 0.2f;
    public LayerMask groundLayer;
    public Collider2D[] OverrlappedGround;

    //Jump
    public float jumpPower = 5f;
    public float fallMultiplayer = 2.5f;
    public float lowJumpMultiplier = 2f;

    //Used in animator
    bool isAttacking;
    bool isThrowing;

    public Collider2D[] FeetColliders;


    DeathController deathController;

    private new Rigidbody2D rigidbody2D;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathController = GetComponent<DeathController>();
        OverrlappedGround = new Collider2D[99];
    }

    // Update is called once per frame
    void Update()
    {
        if (!deathController.IsDead)
        {
            Move();
            Attack();
        }
    }


    //---------------------------------------------------------

    void FixedUpdate()
    {
        IsGrounded();

        //CheckJumpDownPlatform();
    }

    void IsGrounded()
    {
        bool found = false;
        if (rigidbody2D.velocity.y <= 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            foreach (var gc in groundCheck)
            {
                var colliders = Physics2D.OverlapCircleAll(gc.position, groundRadius, groundLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        found = true;
                        animator.SetBool("IsGrounded", grounded = true);
                        //return;
                        OverrlappedGround[i] = colliders[i];
                    }
                }
            }
        }
        if (!found)
            animator.SetBool("IsGrounded", grounded = false);

    }

    void Move()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetBool("IsMoving", moveHorizontal != 0);


        Vector2 jumpVector = CalculateJumpVector();


        rigidbody2D.velocity = new Vector2(moveHorizontal, rigidbody2D.velocity.y) + jumpVector;

        JumpDown();

        Flip();
    }

    //void CheckJumpDownPlatform()
    //{
    //    if (grounded && Input.GetButtonDown("Jump")
    //            && Input.GetAxisRaw("Vertical") < 0)
    //    {
    //        ContactFilter2D filter = new ContactFilter2D();
    //        filter.SetLayerMask(LayerMask.NameToLayer("Platform"));
    //        Collider2D[] results = new Collider2D[99];
    //        var res = rigidbody2D.OverlapCollider(filter, results);

    //        for (int i = 0; i < results.Length; ++i)
    //        {
    //            if (results[i] != null)
    //            {
    //                var platformController = results[i].gameObject.GetComponent<PlatformController>();
    //                if(platformController != null
    //                    && platformController.CanBeDownJumped)
    //                    StartCoroutine(DisableFeetColliders(0.5f));
    //            }
    //            else
    //                return;

    //        }
    //    }
    //}

    void JumpDown()
    {
        if (grounded && Input.GetButtonDown("Jump")
               && Input.GetAxisRaw("Vertical") < 0)
        {
            for (int i = 0; i < OverrlappedGround.Length; ++i)
            {
                if (OverrlappedGround[i] != null)
                {
                    var platformController = OverrlappedGround[i].gameObject.GetComponent<PlatformController>();
                    if (platformController != null
                        && platformController.CanBeDownJumped)
                        platformController.IgnoreCollisionsWith(FeetColliders, 0.5f);
                }
                else
                    return;

            }
        }
    }



    Vector2 CalculateJumpVector()
    {
        var jumpVector = Vector2.zero;
        if (grounded && Input.GetButtonDown("Jump")
            && Input.GetAxisRaw("Vertical") >= 0)//not pressing down
        {
            grounded = false;
            jumpVector = Vector2.up * jumpPower;

            Debug.Log($"Force {Vector2.up * jumpPower}");
        }
        if (rigidbody2D.velocity.y < 0)
        {
            jumpVector += Vector2.up * Physics2D.gravity.y * (fallMultiplayer - 1) * Time.deltaTime;
        }
        else if (rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
            jumpVector += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        return jumpVector;
    }

    void Attack()
    {
        if (Input.GetButton("Stab"))
        {
            Debug.Log("Stab");
            animator.SetBool("Attack_Stab", true);
        }
        else
            animator.SetBool("Attack_Stab", false);

        if (Input.GetButton("Throw"))
            animator.SetBool("Attack_Throw", true);
        else
            animator.SetBool("Attack_Throw", false);
    }

    void AttackStarted()
    {
        isAttacking = true;
        animator.SetBool("IsPerformingAttack", true);
    }

    void AttackFinished()
    {
        isAttacking = false;
        animator.SetBool("IsPerformingAttack", false);
    }

    void ThrowStarted()
    {
        isThrowing = true;
        animator.SetBool("IsPerformingThrow", true);
    }

    void ThrowFinished()
    {
        isThrowing = false;
        animator.SetBool("IsPerformingThrow", false);
    }


    void MarkAsCorpse()
    {
        animator.SetBool("IsCorpse", true);
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