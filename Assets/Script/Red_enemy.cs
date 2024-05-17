using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_enemy : MonoBehaviour
{

    [SerializeField] PlayerController controller;


    //---- Movement ----
    [SerializeField] float speed;

    [SerializeField] Transform[] waypoints;
    private Transform target;
    private int desPoint = 0;

    //---- Player Detection ----
    public bool isPlayer;

    [SerializeField] object Weak_Hitbox_Position;
    [SerializeField] Vector2 Weak_Hitbox_Size;
    [SerializeField] float Player_ColisionLayer;

    void Start()
    {
        target = waypoints[0];
    }


    void Update()
    {
        // ************** Enemy Movement ************** \\

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            desPoint = (desPoint + 1) % waypoints.Length;
            target = waypoints[desPoint];
        }


        // ************** Player Detection ************** \\

        /*Collider2D[] Player_Detection = Physics2D.OverlapBoxAll(Weak_Hitbox_Position, Weak_Hitbox_Size, Player_ColisionLayer);

        isPlayer = false;

        foreach (var Object in Player_Detection)
        {

            if (Object.tag == "Player")
            {
                isPlayer = true;
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("BulletSlim"))
        {
            //Animation EnemyTookSlime
        } 
        
        if (other.gameObject.CompareTag("BulletNet"))
        {
            //Animation EnemyTookNet Stop()
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !controller.Curing)
        {
            //Animation du Stun avec point pour appeler la fonction Stop()
        }

        if (other.CompareTag("Player") && controller.Curing)
        {
            //Animation du BigStun avec point pour appeler la fonction Stop()
        }
    }



    public void Stop()
    {
        //fonction à appeler pendant les animations pour que l'ennemi ne bouge plus
    }*/

    }
}
