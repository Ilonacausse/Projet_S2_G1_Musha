using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player_1 : MonoBehaviour
{
    [SerializeField] float speed = 0.05f;
    [SerializeField] Animator PlayerAnimator;
    [SerializeField] SpriteRenderer SpritePlayer;
    [SerializeField] bool Move = true;


    [SerializeField] ParticleSystem ParticleSystem;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Move) {
            Playermouvement();
        }


    }

    public void Off () {
        Move = false;
        PlayerAnimator.SetBool("Bool_Run", false);

            }

    public void On()
    {
        Move = true;
            }

    void Playermouvement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0, Space.World);             //mettre deltaTime
            PlayerAnimator.SetBool("Bool_Run", true);
            SpritePlayer.flipX = true;

            //ParticuleSystem.Play();                                     //Quand j'appuie sur avancer la trainée se lance
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            PlayerAnimator.SetBool("Bool_Run", false);

            //ParticuleSystem.Stop();                                       //Quand  je n'appuie plus les particules s'arretent
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0, Space.World);
            PlayerAnimator.SetBool("Bool_Run", true);
            SpritePlayer.flipX = false;

            //ParticuleSystem.Play();                                          //Quand j'appuie sur avancer la trainée se lance mais dans l'autre direction sur la verticale
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            PlayerAnimator.SetBool("Bool_Run", false);

            //ParticuleSystem.Stop();                                         //Quand  je n'appuie plus les particules s'arretent
        }
    }
}
