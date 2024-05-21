using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Enemy : MonoBehaviour
{


    [SerializeField] PlayerController controller;


    //---- Movement ----
    [SerializeField] bool isMoving = true;
    [SerializeField] float speed;

    [SerializeField] Transform[] waypoints;
    private Transform target;
    private int desPoint = 0;

    //---- Player Detection ----
    [SerializeField] bool isPlayer; 

    [SerializeField] Transform Weak_Hitbox_Position;
    [SerializeField] Vector2 Weak_Hitbox_Size;
    [SerializeField] LayerMask Player_ColisionLayer;





    void Start()
    {
        target = waypoints[0];
    }



    void Update()
    {

        Move();


     //______________________________________ WEAK POINT ______________________________________

        Collider2D[] Player_Detection = Physics2D.OverlapBoxAll(Weak_Hitbox_Position.position, Weak_Hitbox_Size, Player_ColisionLayer);

        isPlayer = false;

        foreach (var Object in Player_Detection)
        {

            if (Object.tag == "Player" && !controller.Curing)
            {
                //Animation du Stun avec point pour appeler la fonction Stun() puis Unstun()
            }

            if (Object.tag == "Player" && controller.Curing)
            {
                //Animation du BigStun avec point pour appeler la fonction Stun() puis Unstun()
            }
        }
    }

    private void OnDrawGizmos()
    {
        // visuel de la position du cercle sous le joueur 
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Weak_Hitbox_Position.position, Weak_Hitbox_Size);
    }



    //______________________________________ MOVEMENT ______________________________________

    void Move ()
    {
        if (isMoving == true)
        {

            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) < 0.03f)
            {
                desPoint = (desPoint + 1) % waypoints.Length;
                target = waypoints[desPoint];
            }
        }
    }



    //______________________________________ COLLISION ______________________________________

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("BulletSlim"))
        {
            //Animation EnemyTookSlime
        } 
        
        if (other.gameObject.CompareTag("BulletNet"))
        {
            //Animation EnemyTookNet avec Stun() puis Unstun() pour Freeze la position
        }

    }



    //fonction � appeler pendant les animations pour que l'ennemi ne bouge plus
    public void Stun()
    {
        isMoving = false;
    }
    public void Unstun()
    {
        isMoving = true;
    }


}

