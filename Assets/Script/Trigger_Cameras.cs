using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Trigger_Cameras : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] CinemachineVirtualCamera Camera1;
    [SerializeField] CinemachineVirtualCamera Camera2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D()
    {
      Debug.Log("test");
        Camera1.Priority = 1;
        Camera2.Priority = 100;
    }

    private void OnTriggerExit2D()
    {
        Debug.Log("Sortie");
        Camera1.Priority = 100;
        Camera2.Priority = 1;
    }
}
