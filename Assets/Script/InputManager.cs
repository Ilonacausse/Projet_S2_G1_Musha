using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{



    public static InputManager instance;

    public bool MenuOpenCloseInput {  get; private set; }


    private Input_Control _inputControl;
    private InputAction _playerMenuOpenCloseAction;





    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _inputControl = GetComponent<Input_Control>();
   //     _playerMenuOpenCloseAction = InputAction.actions["MenuOpenClose"];
    }

    //private void OnEnable()
    //{
    //    _playerMenuOpenCloseAction.Enable();

    //    _playerMenuOpenCloseAction.started += Jump;
    //}

    //private void OnDisable()
    //{
    //    _playerMenuOpenCloseAction.Disable();
    //}

    private void Update()
    {
        MenuOpenCloseInput = _playerMenuOpenCloseAction.WasPressedThisFrame();
    }
}
