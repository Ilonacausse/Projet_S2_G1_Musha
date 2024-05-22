using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breackable_platform : MonoBehaviour
{


    [SerializeField] PlayerController controller;


    [SerializeField] Rigidbody2D rb;

    [SerializeField] float timing = 0.3f;

    //---- Player Detection ----
    [SerializeField] Transform Break_Hitbox_Position;
    [SerializeField] Vector2 Break_Hitbox_Size;
    [SerializeField] LayerMask Player_ColisionLayer;





    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    void Update()
    {

        //_______________________________________ BREAK POINT ______________________________________

        Collider2D[] Player_Detection = Physics2D.OverlapBoxAll(Break_Hitbox_Position.position, Break_Hitbox_Size, Player_ColisionLayer);


        foreach (var Object in Player_Detection)
        {

            if (Object.tag == "Player" && controller.Curing && controller.isFalling)
            {
                StartCoroutine(FallingPlatform());
            }
        }
    }

    private void OnDrawGizmos()
    {
        // zone de détection ou le joueur atterit pour faire tomber la platforme
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Break_Hitbox_Position.position, Break_Hitbox_Size);
    }

    public IEnumerator FallingPlatform()
    {
        yield return new WaitForSeconds(0.1f);         //Pour avoir  une colision avant que la platform ne se casse

        rb.constraints = RigidbodyConstraints2D.None;   // unlock toutes les positions
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;   //lock les positions qui ne doivent pas bouger 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;     //lock la rotation qui ne doit pas bouger

        yield return new WaitForSeconds(timing);

        Destroy(gameObject);
    }
}
