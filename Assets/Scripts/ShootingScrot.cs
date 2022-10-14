using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScrot : MonoBehaviour
{
    public GameObject ShootingScript;
    public Transform shootingpoint;
    public bool canShoot = true;
    private void Start()
    {
  
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    void shoot()
    {
        if (!canShoot)
            return;
        GameObject si = Instantiate(ShootingScript, shootingpoint);
        si.transform.parent = null;
    }
}
