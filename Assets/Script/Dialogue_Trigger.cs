using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;





public class Dialogue_Trigger : MonoBehaviour
{



    public Dialogue dialogue;

    public bool isInRange;

    //-------- New Input Controller ------------
    [SerializeField] PlayerController controller;
    private Input_Control _inputControl;
    private InputAction _playerInteract;




    /*
    private void Awake()
    {
        _inputControl = new Input_Control();

        _playerInteract = _inputControl.Player.Interact;
    }

    private void OnEnable()
    {
        _playerInteract.Enable();

        _playerInteract.started += Interact;
    }

    private void OnDisable()
    {
        _playerInteract.Disable();
    }




    //______________________________________ PLAYER DETECTION ______________________________________


    void Update ()
    {

        Interact();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            controller.isGrounded = false;
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

    void Interact(InputAction.CallbackContext context)
    {
        if (isInRange)
        {
            Debug.Log("Interraction");
            //        TriggerDialogue();
        }
    }*/
}
