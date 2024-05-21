using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breackable_platform : MonoBehaviour
{


    [SerializeField] PlayerController controller;

    [SerializeField] Rigidbody2D rb;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


}
