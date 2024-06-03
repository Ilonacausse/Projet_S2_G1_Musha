using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;

public class MenuManager : MonoBehaviour
{



    [SerializeField] private GameObject _menuPauseCanvas;
    [SerializeField] private GameObject _menuSettingsCanvas;

    private bool isPaused;

    [SerializeField] private PlayerController _player;

    [SerializeField] private GameObject _menuPauseFirst;
    [SerializeField] private GameObject _settingsMenuFirts;





    private void Start()
    {
        _menuPauseCanvas.SetActive(false);
        _menuSettingsCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"));
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }




    #region Pause/Unpause Functions

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        _player.enabled = false;

        OpenMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        _player.enabled = true;

        CloseMenu();
    }

    #endregion



    #region Canvas Activatetions/Deactivations

    private void OpenMenu()
    {
        _menuPauseCanvas.SetActive(true);
        _menuSettingsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_menuPauseFirst);
    }

    private void OpenSettingsMenu()
    {
        _menuPauseCanvas.SetActive(false);
        _menuSettingsCanvas.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_settingsMenuFirts);
    }


    private void CloseMenu()
    {
        _menuPauseCanvas.SetActive(false);
        _menuSettingsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    #endregion



    #region Menu Button Actions

    public void OnSettingsPress()
    {
        OpenSettingsMenu();
    }

    public void OnResumePress()
    {
        Unpause();
    }

    #endregion



    #region Setting Menu Button Actions

    public void OnSettingsBackPress()
    {
        OpenMenu();
    }

    #endregion
}