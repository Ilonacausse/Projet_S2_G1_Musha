using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;





public class PlayerController : MonoBehaviour
{


    //-------- New Input Controller ------------
    private Input_Control _inputControl;

    private InputAction _playerMove;
    private InputAction _playerPower1;
    private InputAction _playerPower2;
    private InputAction _playerPower3;
    private InputAction _playerJump;
    


    //-------- Player can move or not ------------
    public bool CanMove = true;

    //-------- Movement standard ------------
    [SerializeField] PlayerController _playerController;

    [SerializeField] float Speed = 5f;

    public Vector2 Input_Direction;



    //-------- Player Jump ------------
    [SerializeField] Rigidbody2D _playerRB;
    [SerializeField] float PlayerJump = 250f;
    [SerializeField] int NbJump = 1;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius; 
    public bool isGrounded;
    [SerializeField] LayerMask CollisionsLayers;

    public bool isFalling = false;



    //-------- Powers ------------
    public Transform BulletSpawner;
    private Transform Gun_Transform;


    //---- Curing ----
    public bool Curing = false;
    [SerializeField] float mass;
    public bool curingpower = false;

    //---- Slime ----
    [SerializeField] GameObject bulletPrefab_slime;
    [SerializeField] float bulletSpeed_slime = 10;
    [SerializeField] float Ammo_slime = 6;
    [SerializeField] float Timer_slime = 10f;
    public bool slimepower = false;


    //---- Net ----
    [SerializeField] GameObject bulletPrefab_net;
    [SerializeField] float bulletSpeed_net = 10;
    [SerializeField] float Ammo_net = 3;
    [SerializeField] float Timer_net = 15f;
    public bool netpower = false;



    //-------- Animator and sprite renderer ------------
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    public bool AxeX;
    public bool isMoving;





    void Start ()
    {
        curingpower = false;
        slimepower = false;
        netpower = false;
    }



    private void Awake()
    {
        _inputControl = new Input_Control();

        _playerMove = _inputControl.Player.Move;
        _playerPower1 = _inputControl.Player.Power1;
        _playerPower2 = _inputControl.Player.Power2;
        _playerPower3 = _inputControl.Player.Power3;
        _playerJump = _inputControl.Player.Jump;

        _playerRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerMove.Enable();
        _playerPower1.Enable();
        _playerPower2.Enable();
        _playerPower3.Enable();
        _playerJump.Enable();

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
    }



    private void FixedUpdate()
    {
        PlayerMouvement();
        Falling();


        // ************** Ground Detection ************** \\

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, CollisionsLayers);

        if (isGrounded && _playerRB.velocity.y >= -0.05f && _playerRB.velocity.y <= 0.05f)
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
            Vector2 Player_Velocity = _playerRB.velocity;

            Player_Velocity.x = (Speed * Input_Direction.x);
            _playerRB.velocity = Player_Velocity;


            if (_playerRB.velocity.x > 0.01)
            {

               // GetComponent<SpriteRenderer>().flipX = true;
                this.transform.Find("Gun").rotation = Quaternion.Euler(0f, 0f, 0f);        //Flip the Gun when the player changes his axe
                AxeX = true;        //Si l'axe est positif
                isMoving = true;    //Si la velocity n'est pas nulle, qu'il bouge

            }
            else if (_playerRB.velocity.x < -0.01)
            {

               // GetComponent<SpriteRenderer>().flipX = false;
                this.transform.Find("Gun").rotation = Quaternion.Euler(0f, 180f, 0f);        //Flip the Gun when the Player changes his axe
                AxeX = false;
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            Animation();
        }

    }

    void PlayerCanMove()
    {
       CanMove = true;
    }



    //______________________________________ JUMP - INTERACTION ______________________________________

    public void Jump(InputAction.CallbackContext context)
    {
        if (NbJump > 0 && isGrounded)
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, PlayerJump);
            NbJump = NbJump - 1;
        }

        Animation();
    }


    public void Falling()
    {
        // ************** Breackable Platform ************** \\

        if (_playerRB.velocity.y >= 0f)
        {
            isFalling = false;
        }
        if (_playerRB.velocity.y <= -0.6f)
        {
            isFalling = true;
        }
    }


    private void Animation()
    {
        // ************** Animation Idle ************** \\

        if (isGrounded && AxeX && !isMoving)
        {
            anim.SetBool("IdleD", true);
            anim.SetBool("IdleG", false);
        }
        else if (isGrounded && !AxeX && !isMoving)
        {
            anim.SetBool("IdleD", false);
            anim.SetBool("IdleG", true);
        }
        else
        {
            anim.SetBool("IdleD", false);
            anim.SetBool("IdleG", false);
        }


        // ************** Animation Run ************** \\

        if (isGrounded && AxeX && isMoving)
        {
            anim.SetBool("isRunningD", true);
            anim.SetBool("isRunningG", false);
        }
        else if (isGrounded && !AxeX && isMoving)
        {
            anim.SetBool("isRunningD", false);
            anim.SetBool("isRunningG", true);
        }
        else
        {
            anim.SetBool("isRunningD", false);
            anim.SetBool("isRunningG", false);
        }



        // ************** Animation Jump ************** \\

        if (!isGrounded && AxeX)
        {
            anim.SetBool("isJumpingD", true);
            anim.SetBool("isJumpingG", false);
        }
        else if (!isGrounded && !AxeX)
        {
            anim.SetBool("isJumpingD", false);
            anim.SetBool("isJumpingG", true);
        }
        else
        {
            anim.SetBool("isJumpingD", false);
            anim.SetBool("isJumpingG", false);
        }
    }


    //______________________________________ CURING ______________________________________

    void CuringPower(InputAction.CallbackContext context)
    {
        if (curingpower == true)
        {
            if (Curing == false)
            {
                _playerRB.mass = 10;
                PlayerJump = 9;
                Speed = 3;

                Curing = true;

            }
            else if (Curing == true)
            {
                _playerRB.mass = 1.08f;
                PlayerJump = 16.5f;
                Speed = 7;

                Curing = false;
            }
        }


    }



    //______________________________________ SLIME ______________________________________

    private void SlimePower(InputAction.CallbackContext context)
    {

        if (slimepower == true)
        {

            if (Ammo_slime >= 0 && _playerController.Curing == false && isGrounded)
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

    }



    //______________________________________ NET ______________________________________

    private void NetPower(InputAction.CallbackContext context) 
    {

        if (netpower == true)
        {

            if (Ammo_net >= 0 && _playerController.Curing == false && isGrounded)
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
    }




    //______________________________________ HEALTH - GAMEOVER ______________________________________

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            HealthSystem.health--;
            if(HealthSystem.health <= 0)
            {
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }
    }
    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(3,7);
        yield return new WaitForSeconds(1.5f);     //un temps d'invulnerabilite
        Physics2D.IgnoreLayerCollision(3, 7, false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            Debug.Log("Check");
            PlayerManager._lastCheckPoint = transform.position;
            collision.GetComponent<Collider2D>().enabled = false;     //Pour ne plus intéragir apres
            collision.GetComponent<Animator>().SetTrigger("isAppear");   //Trigger Checkpoint animation
        }


        //-------- Curing ------------

        if (collision.CompareTag("NPCgano"))
        {
            curingpower = true;
        }


        //-------- Slime ------------

        if (collision.CompareTag("NPCcoprine"))
        {
            slimepower = true;
        }


        //-------- Curing ------------

        if (collision.CompareTag("NPCci"))
        {
            netpower = true;
        }
    }
}

