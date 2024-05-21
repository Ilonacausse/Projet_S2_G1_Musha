using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;





public class PlayerController : MonoBehaviour
{


    //-------- New Input Controller ------------
    private Input_Control _inputControl;

    private InputAction _playerMove;
    private InputAction _playerPower1;
    private InputAction _playerPower2;
    private InputAction _playerPower3;
    private InputAction _playerJump;
    private InputAction _playerInteract;
    


    //-------- Player can move or not ------------
    public bool CanMove = true;

    //-------- Movement standard ------------
    [SerializeField] PlayerController playerController;

    [SerializeField] float Speed = 5f;

    public Vector2 Input_Direction;


    //-------- Player Jump ------------
    [SerializeField] Rigidbody2D PlayerRigidBody;
    [SerializeField] float PlayerJump = 250f;
    [SerializeField] int NbJump = 1;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius; 
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask CollisionsLayers;


    //-------- Powers ------------
    public Transform BulletSpawner;
    private Transform Gun_Transform;

    public bool Curing = false;

    //---- Slime ----
    [SerializeField] GameObject bulletPrefab_slime;
    [SerializeField] float bulletSpeed_slime = 10;
    [SerializeField] float Ammo_slime = 6;
    [SerializeField] float Timer_slime = 10f;

    //---- Net ----
    [SerializeField] GameObject bulletPrefab_net;
    [SerializeField] float bulletSpeed_net = 10;
    [SerializeField] float Ammo_net = 3;
    [SerializeField] float Timer_net = 15f;

    //---- Curing ----
    [SerializeField] float mass;


    //-------- Player Animator and sprite renderer ------------
    [SerializeField] Animator Player_Animator;
    [SerializeField] SpriteRenderer spriteRenderer;


    //-------- Health ------------

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth = 3;

    public static PlayerController instance;





    void Start()
    {
        currentHealth = maxHealth;
    }



    private void Awake()
    {
        _inputControl = new Input_Control();

        _playerMove = _inputControl.Player.Move;
        _playerPower1 = _inputControl.Player.Power1;
        _playerPower2 = _inputControl.Player.Power2;
        _playerPower3 = _inputControl.Player.Power3;
        _playerJump = _inputControl.Player.Jump;
        _playerInteract = _inputControl.Player.Interact;

        PlayerRigidBody = GetComponent<Rigidbody2D>();


        instance = this;
    }

    private void OnEnable()
    {
        _playerMove.Enable();
        _playerPower1.Enable();
        _playerPower2.Enable();
        _playerPower3.Enable();
        _playerJump.Enable();
        _playerInteract.Enable();

        _playerJump.started += Jump;
        _playerPower1.started += SlimePower;
        _playerPower2.started += NetPower;
        _playerPower3.started += CuringPower;
    }

    private void OnDisable()
    {
        _playerMove.Disable();
        _playerPower1.Disable();
        _playerPower2.Disable();
        _playerPower3.Disable();
        _playerJump?.Disable();
        _playerInteract.Disable();
    }



    private void FixedUpdate()
    {
        PlayerMouvement();

        // ************** Ground Detection ************** \\

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, CollisionsLayers);

        if (isGrounded && PlayerRigidBody.velocity.y >= -0.05f && PlayerRigidBody.velocity.y <= 0.05f)
        {
            NbJump = 1;
        }

    }
    private void OnDrawGizmos()
    {
        // visuel de la position du cercle sous le joueur 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


    //______________________________________ MOVEMENT ______________________________________

    public void PlayerMouvement()
    {
        if (CanMove)
        {
            Input_Direction = _playerMove.ReadValue<Vector2>();
            Vector2 Player_Velocity = PlayerRigidBody.velocity;

            Player_Velocity.x = (Speed * Input_Direction.x);
            PlayerRigidBody.velocity = Player_Velocity;


            if (PlayerRigidBody.velocity.x > 0.01)
            {

               // GetComponent<SpriteRenderer>().flipX = true;
                this.transform.Find("Gun").rotation = Quaternion.Euler(0f, 0f, 0f);        //Flip the Gun when the player changes his axe

            }
            else if (PlayerRigidBody.velocity.x < -0.01)
            {

               // GetComponent<SpriteRenderer>().flipX = false;
                this.transform.Find("Gun").rotation = Quaternion.Euler(0f, 180f, 0f);        //Flip the Gun when the Player changes his axe

            }

        }

    }

    void PlayerCanMove()
    {
       CanMove = true;
    }



    //______________________________________ JUMP ______________________________________

    public void Jump(InputAction.CallbackContext context)
    {
        if (NbJump > 0 && isGrounded)
        {
            PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, PlayerJump);
            NbJump = NbJump - 1;
        }

    }



    //______________________________________ SLIME ______________________________________

    private void SlimePower(InputAction.CallbackContext context)
    {

        if (Ammo_slime >= 0 && playerController.Curing == false)
        {
            var bullet = Instantiate(bulletPrefab_slime, BulletSpawner.position, BulletSpawner.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawner.forward * bulletSpeed_slime;
            Ammo_slime -= 1;
            Debug.Log(Ammo_slime);
        }

        if (Ammo_slime == 0)
        {
            StartCoroutine(AmmoRefiled(Timer_slime));

        }

        IEnumerator AmmoRefiled(float timer)
        {
            //instantiation des projectiles grace aux prefabs avec une coroutine pour creer un timing entre chaque projectile 
            yield return new WaitForSeconds(timer);

            Ammo_slime = 7;

        }

    }



    //______________________________________ NET ______________________________________

    private void NetPower(InputAction.CallbackContext context) 
    { 

        if (Ammo_net >= 0 && playerController.Curing == false)
        {
            var bullet = Instantiate(bulletPrefab_net, BulletSpawner.position, BulletSpawner.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawner.forward * bulletSpeed_net;
            Ammo_net -= 1;
            Debug.Log(Ammo_net);
        }

        if (Ammo_net == 0)
        {
            StartCoroutine(AmmoRefiled_net(Timer_net));

        }

        IEnumerator AmmoRefiled_net(float timer)
        {
            //instantiation des projectiles grace aux prefabs avec une coroutine pour creer un timing entre chaque projectile 
            yield return new WaitForSeconds(timer);

            Ammo_net = 3;

        }
    }



    //______________________________________ CURING ______________________________________

    void CuringPower(InputAction.CallbackContext context)
    {

        if (Curing == false)
        {
            PlayerRigidBody.mass = 10;
            PlayerJump = 9;
            Speed = 3;

            Curing = true;
        }
        else if (Curing == true)
        {
            PlayerRigidBody.mass = 1.08f;
            PlayerJump = 16.5f;
            Speed = 5;

            Curing = false;
        }
    }

}

