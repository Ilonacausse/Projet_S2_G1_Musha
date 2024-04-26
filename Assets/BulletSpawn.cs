using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public Transform BulletSpawner;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float countdown = 1;
    public float initialValue = 1;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
        var bullet = Instantiate(bulletPrefab, BulletSpawner.position, BulletSpawner.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawner.forward * bulletSpeed;
        }
    }
}


