using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //--------Player can moove or not ------------
    public bool CanMove = true;

    //--------Movement standard and dash values ------------
    [SerializeField] float CurrentSpeed;
    [SerializeField] float NormalSpeed = 5f;
    [SerializeField] float dashingSpeed = 100f;

    //--------Movement and dash values ------------

    private bool canDash = true;
    [SerializeField] float dashingTime = 0.1f;
    [SerializeField] float dashCooldown = 2f;

    //--------Player Animator and sprite renderer ------------
    [SerializeField] Animator Player_Animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    //--------Player Jump ------------
    [SerializeField] Rigidbody2D PlayerRigidBody;
    [SerializeField] float PlayerJump= 250f;
      
    [SerializeField] bool isGrounded;
    [SerializeField] bool isJumping;
    [SerializeField] LayerMask CollisionsLayers;

    [SerializeField] float groundCheckRadius;
    [SerializeField] Transform groundCheck;

    //--------Particule Systeme ------------
    [Header("Particule")]
    [SerializeField] ParticleSystem psJump;
    [SerializeField] ParticleSystem psRun;

    void Start()
    {
        //set player startup speed
        CurrentSpeed = NormalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMouvement();
        Jump();

    }

    void FixedUpdate()
    {
        // cercle pour détecter si le joueur touche le cercle ( méthode du overlapcircle pour pas créer un gameobject réel dans la scène)
        // rendu position et couleur du circle appelé dans la fonction OnDrawGizmos
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, CollisionsLayers);

        if (isJumping == true)
        {
            PlayerRigidBody.AddForce(new Vector2(0f, PlayerJump));
            isJumping = false;
            psRun.Stop();
        }
    }

    void PlayerMouvement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && CanMove)
        {
            transform.Translate(Vector2.left * CurrentSpeed * Time.deltaTime);
            //PlayerRigidBody.MovePosition(PlayerRigidBody.position + (Vector2.left * CurrentSpeed * Time.deltaTime));
            Player_Animator.SetBool("BoolRun", true);
            spriteRenderer.flipX = true;

            if (isGrounded)
            {
                psRun.Play();
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow) && CanMove)
        {
            transform.Translate(Vector2.right * CurrentSpeed * Time.deltaTime);
            //PlayerRigidBody.MovePosition(PlayerRigidBody.position + (Vector2.right * CurrentSpeed * Time.deltaTime));
            Player_Animator.SetBool("BoolRun", true);
            spriteRenderer.flipX = false;

           if (isGrounded)
            {
                psRun.Play();
            }
        }
        else
        {
            Player_Animator.SetBool("BoolRun", false);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Player_Animator.SetTrigger("TrCrouch");
        }

        else if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            //
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Player_Animator.SetTrigger("TrSeek");
        }

        else if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            //
        }

        if (Input.GetKeyDown(KeyCode.F) && canDash)
        {
            Dash();
        }

    }

    public void PlayerCanMove()
    {
       CanMove = true;
    }

void Dash()
    {
        canDash = false;
        CurrentSpeed = dashingSpeed;
        StartCoroutine(PlayerDash());
    }

    private IEnumerator PlayerDash()
    {
        yield return new WaitForSeconds(dashingTime);
        CurrentSpeed = NormalSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Player_Animator.SetTrigger("TrJump");
            Player_Animator.SetBool("BoolRun", false);
            isJumping = true;
            psJump.Play();
        }

    }


    private void OnDrawGizmos()
    {
        // rendu et position du cercle sous le joueur 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

 
    //Coin wip
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //CM.coinCount++;
            Destroy(gameObject);
        }
    }
}

