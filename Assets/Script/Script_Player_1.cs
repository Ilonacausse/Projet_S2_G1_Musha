using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player_1 : MonoBehaviour
{
    float speed = 6;
    [SerializeField] Animator PlayerAnimator;
    [SerializeField] SpriteRenderer SpritePlayer;


    [SerializeField] ParticleSystem grass;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Playermouvement();

    }


    void Playermouvement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);             //mettre deltaTime
            PlayerAnimator.SetBool("Bool_Run", true);
            SpritePlayer.flipX = true;
            //grass.Play();
                                    //Quand j'appuie sur avancer la trainée se lance
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            PlayerAnimator.SetBool("Bool_Run", false);
            //grass.Stop();
                                       //Quand  je n'appuie plus les particules s'arretent
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
            PlayerAnimator.SetBool("Bool_Run", true);
            SpritePlayer.flipX = false;
            //grass.Play();
                                     //Quand j'appuie sur avancer la trainée se lance mais dans l'autre direction sur la verticale
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            PlayerAnimator.SetBool("Bool_Run", false);
      /*      grass.Stop();  */                          //Quand  je n'appuie plus les particules s'arretent

        }

    }
} 
