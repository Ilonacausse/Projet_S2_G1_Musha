using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Dialogue_Trigger : MonoBehaviour
{



    public Dialogue dialogue;

    [SerializeField] PlayerController controller;

    public bool isInRange;





    //______________________________________ PLAYER DETECTION ______________________________________

    void Update ()
    {
        if(isInRange) 
        {
            TriggerDialogue();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }



    //______________________________________ START DIALOGUE ______________________________________

    void TriggerDialogue ()
    {

    }
}
