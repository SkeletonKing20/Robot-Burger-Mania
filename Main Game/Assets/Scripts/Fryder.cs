using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fryder : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spawnPos;
    private float fireRate = 1f;
    private float nextFire = 0f;

    private void Update()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shoot();
        }
    }
    public void shoot()
    {
        Instantiate(projectile, spawnPos.transform.position, transform.rotation);
    }
}
