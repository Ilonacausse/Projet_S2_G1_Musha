using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{



    public static bool isGameOver;
    public GameObject _gameoverScreen;

    public static Vector2 _lastCheckPoint = new Vector2(-25.29f,-3);


    [SerializeField] private GameObject _replayMenuFirts;





    private void Awake()
    {
        isGameOver = false;
        _gameoverScreen.SetActive(false);


        GameObject.FindGameObjectWithTag("Player").transform.position = _lastCheckPoint;
    }


    void Update ()
    {
        if (isGameOver)
        {
            _gameoverScreen.SetActive(true);
        }
    }


    public void Replay ()
    {
        EventSystem.current.SetSelectedGameObject(_replayMenuFirts);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
