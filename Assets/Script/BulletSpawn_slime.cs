using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public Transform BulletSpawner;
    [SerializeField] PlayerController playerController;


    //---------- Slime ------------
    [SerializeField] GameObject bulletPrefab_slime;
    [SerializeField] float bulletSpeed_slime = 10;
    [SerializeField] float Ammo_slime = 6;
    [SerializeField] float Timer_slime = 10f;


    //---------- Net ------------
    [SerializeField] GameObject bulletPrefab_net;
    [SerializeField] float bulletSpeed_net = 10;
    [SerializeField] float Ammo_net = 3;
    [SerializeField] float Timer_net = 15f;


    void Update()
    {
        /*************** SLIME ***************/

        if (Input.GetKeyDown(KeyCode.Keypad1) && Ammo_slime >= 0 && playerController.Curing == false)
        {
            var bullet = Instantiate(bulletPrefab_slime, BulletSpawner.position, BulletSpawner.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawner.forward * bulletSpeed_slime;
            Ammo_slime -= 1;
            Debug.Log(Ammo_slime);
        }

        if (Ammo_slime == 0)
        {
            StartCoroutine(AmmoRefiled(Timer_slime));

        }

        IEnumerator AmmoRefiled(float timer)
        {
            //instantiation des projectiles grace aux prefabs avec une coroutine pour creer un timing entre chaque projectile 
            yield return new WaitForSeconds(timer);

            Ammo_slime = 7;
            //StartCoroutine(AmmoRefiled(Timer_slime));

        }

        /*************** NET ***************/

        if (Input.GetKeyDown(KeyCode.Keypad2) && Ammo_net >= 0 && playerController.Curing == false)
        {
            var bullet = Instantiate(bulletPrefab_net, BulletSpawner.position, BulletSpawner.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawner.forward * bulletSpeed_net;
            Ammo_net -= 1;
            Debug.Log(Ammo_net);
        }

        if (Ammo_net == 0)
        {
            StartCoroutine(AmmoRefiled_net(Timer_net));

        }

        IEnumerator AmmoRefiled_net(float timer)
        {
            //instantiation des projectiles grace aux prefabs avec une coroutine pour creer un timing entre chaque projectile 
            yield return new WaitForSeconds(timer);

            Ammo_net = 3;
            //StartCoroutine(AmmoRefiled(Timer_net));

        }

    }






}





