using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound_Platform : MonoBehaviour
{



    [SerializeField] Rigidbody2D rb;

    public GameObject _link;


    //---- Player Detection ----
    [SerializeField] Transform Break_Hitbox_Position;
    [SerializeField] Vector2 Break_Hitbox_Size;
    [SerializeField] LayerMask Slime_ColisionLayer;





    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _link = GameObject.FindWithTag("Link");

    }



    void Update()
    {

        //_______________________________________ BREAK POINT ______________________________________

        Collider2D[] Slime_Detection = Physics2D.OverlapBoxAll(Break_Hitbox_Position.position, Break_Hitbox_Size, Slime_ColisionLayer);


        foreach (var Object in Slime_Detection)
        {

            if (Object.tag == "BulletSlime")
            {
                StartCoroutine(FallingPlatform());
            }
        }
    }

    private void OnDrawGizmos()
    {   
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(Break_Hitbox_Position.position, Break_Hitbox_Size);
    }

    public IEnumerator FallingPlatform()
    {
        yield return new WaitForSeconds(0.1f);         //Pour avoir  une colision avant que la platform ne se casse

        rb.constraints = RigidbodyConstraints2D.None;   // unlock toutes les positions
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;   //lock les positions qui ne doivent pas bouger 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;     //lock la rotation qui ne doit pas bouger

        Destroy(_link);
    }
}