using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Main_Menu : MonoBehaviour
{
    public string levelToLoad;
    public void StartButton()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void SettingButton()
    {

    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
