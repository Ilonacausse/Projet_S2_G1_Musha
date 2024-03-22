using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStick : MonoBehaviour
{

    [SerializeField] float speed = 10f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StickMovement();
    }


    void StickMovement()
    {
        transform.Rotate(0, 0, 25f * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate( 0, speed * Time.deltaTime, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate( 0,-speed * Time.deltaTime, 0, Space.World);
        }
    }
}
