using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_Script : MonoBehaviour
{


    [SerializeField] Collider2D collider;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayerManager._lastCheckPoint = transform.position;
            GetComponent<Collider2D>().enabled = false;     //Pour ne plus intéragir apres
            GetComponent<Animator>().SetTrigger("isAppear");   //Trigger Checkpoint animation
        }
    }
}
