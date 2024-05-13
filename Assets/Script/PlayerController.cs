using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //--------Player can moove or not ------------
    public bool CanMove = true;

    //--------Movement standard ------------
    private Input_Manager _defaultMovePlayer;

    [SerializeField] float CurrentSpeed;
    [SerializeField] float NormalSpeed = 5f;

    private Transform Gun_Transform;
    public bool Curing = false;

    //--------Player Animator and sprite renderer ------------
    [SerializeField] Animator Player_Animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    //--------Player Jump ------------
    [SerializeField] Rigidbody2D PlayerRigidBody;
    [SerializeField] float PlayerJump= 250f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask collisionLayer;
      
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask CollisionsLayers;



    private void Awake()
    {
        _defaultMovePlayer = new Input_Manager();
    }

    private void OnEnable ()
    {
        _defaultMovePlayer.Player.Move.Enable();
        _defaultMovePlayer.Player.Jump.Enable();
        _defaultMovePlayer.Player.Power.Enable();
        _defaultMovePlayer.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        _defaultMovePlayer.Player.Move.Disable();
        _defaultMovePlayer.Player.Jump.Disable();
        _defaultMovePlayer.Player.Power.Disable();
        _defaultMovePlayer.Player.Interact.Disable();
    }

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
        Curing_Power();

    }

    private void FixedUpdate()
    {
        
        // ************** Ground Detection ************** \\

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, CollisionsLayers);
    }
    private void OnDrawGizmos()
    {
        // rendu et position du cercle sous le joueur 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


    // ************** Movement ************** \\

    void PlayerMouvement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && CanMove)
        {
            transform.Translate(Vector2.left * CurrentSpeed * Time.deltaTime);
            //PlayerRigidBody.MovePosition(PlayerRigidBody.position + (Vector2.left * CurrentSpeed * Time.deltaTime));
            //Player_Animator.SetBool("BoolRun", true);
            GetComponent<SpriteRenderer>().flipX = true;
            this.transform.Find("Gun").rotation = Quaternion.Euler(0f, 180f, 0f);                                         //Flip the Gun when the player changes his axe

        }

        else if (Input.GetKey(KeyCode.RightArrow) && CanMove)
        {
            transform.Translate(Vector2.right * CurrentSpeed * Time.deltaTime);
            //PlayerRigidBody.MovePosition(PlayerRigidBody.position + (Vector2.right * CurrentSpeed * Time.deltaTime));
            // Player_Animator.SetBool("BoolRun", true);
            GetComponent<SpriteRenderer>().flipX = false;
            this.transform.Find("Gun").rotation = Quaternion.Euler(0f, 0f, 0f);                                         //Flip the Gun when the Player changes his axe

        }

    }

    public void PlayerCanMove()
    {
       CanMove = true;
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRigidBody.AddForce(new Vector2(0f, PlayerJump));
            //Player_Animator.SetTrigger("TrJump");
            //Player_Animator.SetBool("BoolRun", false);
            //psJump.Play();
        }

    }

    void Curing_Power()
    {


        if (Input.GetKey(KeyCode.Keypad3))
        {
            Curing = true;

        }
    }

}

