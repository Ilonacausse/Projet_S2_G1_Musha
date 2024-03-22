using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float StartTime = 0f;
    [SerializeField] float CurrentTime = 100f;
    [SerializeField] TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        StartTime = CurrentTime;
    }

    // Update is called once per frame
    void Update()
    {
        TIMER();
    }

    void TIMER()
    {
        
        CurrentTime -= 1 * Time.deltaTime;
        countdownText.text = CurrentTime.ToString("0");

        if (CurrentTime <=0)
        {
            CurrentTime = 0;

            SceneManager.LoadScene("MainMenu");
        }
    }
}
