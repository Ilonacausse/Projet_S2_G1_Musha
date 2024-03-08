using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_weakPoint : MonoBehaviour
{

    public GameObject objectToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(objectToDestroy);
        }
    }
}
