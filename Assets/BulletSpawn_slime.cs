using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public Transform BulletSpawner_R;
    public Transform BulletSpawner_L;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float countdown = 1;
    public float Ammo = 7;
    public float Timer = 10f;
    private bool FacingRight;
    private bool FacingLeft;


    private void Start()
    {
        BulletSpawner_R.gameObject.SetActive(true);
        BulletSpawner_L.gameObject.SetActive(false);
    }


    void Update()
    {

        if (FacingRight == true)
        {
            BulletSpawner_L.gameObject.SetActive( false );
            BulletSpawner_R.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.UpArrow) && Ammo >= 0)
            {
                var bullet = Instantiate(bulletPrefab, BulletSpawner_R.position, BulletSpawner_R.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawner_R.forward * bulletSpeed;
                Ammo -= 1;
                Debug.Log(Ammo);
            }
        }


        if (FacingLeft == true)
        {
            BulletSpawner_R.gameObject.SetActive(false);
            BulletSpawner_L.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.UpArrow) && Ammo >= 0)
            {
                var bullet = Instantiate(bulletPrefab, BulletSpawner_L.position, BulletSpawner_L.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawner_L.forward * bulletSpeed;
                Ammo -= 1;
                Debug.Log(Ammo);
            }
        }
        if (Ammo == 0) 
        {
            StartCoroutine(AmmoRefiled(Timer));

        }
    }




    void lookLeft()
    {
        ///////////////Si il va a Gauche
        FacingLeft = true;
    }

    void lookRight()
    {
        /////////////////Si il va a droite
        FacingRight = true;
    }


    IEnumerator AmmoRefiled(float timer)
    {
        //instantiation des fleches grace au prefab avec une coroutine pour cree un timing entre chaque fleche 
        yield return new WaitForSeconds(timer);

        Ammo = 7;
        StartCoroutine(AmmoRefiled(Timer));
    }

}



