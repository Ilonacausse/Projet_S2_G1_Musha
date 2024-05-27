using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayerManager._lastCheckPoint = transform.position;
        }
    }
}
