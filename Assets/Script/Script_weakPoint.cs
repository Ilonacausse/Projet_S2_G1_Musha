using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_weakPoint : MonoBehaviour
{

    public GameObject objectToDestroy;

    [SerializeField] PlayerController controller;


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
    }

}
