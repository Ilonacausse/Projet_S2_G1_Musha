using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    private Input_Control _inputControl;

    private InputAction _playerMove;
    private InputAction _playerPower1;
    private InputAction _playerPower2;
    private InputAction _playerPower3;
    private InputAction _playerJump;
    private InputAction _playerInteract;
    


    //--------Player can move or not ------------
    public bool CanMove = true;

    //--------Movement standard ------------
    public Transform BulletSpawner;
    [SerializeField] PlayerController playerController;

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
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask CollisionsLayers;

    //---------- Slime ------------
    [SerializeField] GameObject bulletPrefab_slime;
    [SerializeField] float bulletSpeed_slime = 10;
    [SerializeField] float Ammo_slime = 6;
    [SerializeField] float Timer_slime = 10f;


    //---------- Net ------------
    [SerializeField] GameObject bulletPrefab_net;
    [SerializeField] float bulletSpeed_net = 10;
    [SerializeField] float Ammo_net = 3;
    [SerializeField] float Timer_net = 15f;



    public Vector2 Input_Direction;

    private void Awake()
    {
        _inputControl = new Input_Control();

        _playerMove = _inputControl.Player.Move;
        _playerPower1 = _inputControl.Player.Power1;
        _playerPower2 = _inputControl.Player.Power2;
        _playerPower3 = _inputControl.Player.Power3;
        _playerJump = _inputControl.Player.Jump;
        _playerInteract = _inputControl.Player.Interact;
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



    void Start()
    {
        CurrentSpeed = NormalSpeed;
    }



    private void FixedUpdate()
    {
        PlayerMouvement();

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

    public void PlayerMouvement()
    {
        if (CanMove)
        {
            Input_Direction = _playerMove.ReadValue<Vector2>();
            Vector2 Player_Velocity = PlayerRigidBody.velocity;

            Player_Velocity.x = (CurrentSpeed * Input_Direction.x);
            PlayerRigidBody.velocity = Player_Velocity;

            if (PlayerRigidBody.velocity.x > 0.01)
            {


               // GetComponent<SpriteRenderer>().flipX = true;
                this.transform.Find("Gun").rotation = Quaternion.Euler(0f, 0f, 0f);          //Flip the Gun when the player changes his axe

            }
            else if (PlayerRigidBody.velocity.x < -0.01)
            {


               // GetComponent<SpriteRenderer>().flipX = false;
                this.transform.Find("Gun").rotation = Quaternion.Euler(0f, 180f, 0f);          //Flip the Gun when the Player changes his axe

            }

        }

    }

    void PlayerCanMove()
    {
       CanMove = true;
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            PlayerRigidBody.AddForce(new Vector2(0f, PlayerJump));
        }

    }


    /*************** SLIME ***************/

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
            //StartCoroutine(AmmoRefiled(Timer_slime));

        }

    }


    /*************** NET ***************/

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
            //StartCoroutine(AmmoRefiled(Timer_net));

        }
    }


    /*************** CURING ***************/

    void CuringPower(InputAction.CallbackContext context)
    {


        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Curing = true;
        }
    }

}

