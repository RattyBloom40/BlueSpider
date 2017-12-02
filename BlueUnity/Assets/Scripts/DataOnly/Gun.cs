using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun {
    float fireRate;
    float dmg;
    float bulletSpeed;
    int maxAmmo;
    int currentAmmo;

    public void Reload() {
        currentAmmo = maxAmmo;
    }

    public void Fire() {
        currentAmmo--;
    }

    public Gun(float fireRate, float dmg, float bulletSpeed, int maxAmmo)
    {
        this.fireRate = fireRate;
        this.dmg = dmg;
        this.bulletSpeed = bulletSpeed;
        currentAmmo = this.maxAmmo = maxAmmo;
    }

    public static Gun pistol = new Gun(2, 10, 10, 5);


    // Atributes

    public float FireRate {
        get {
            return fireRate;
        }
    }

    public float Dmg {
        get {
            return dmg;
        }
    }

    public float BulletSpeed {
        get {
            return bulletSpeed;
        }
    }

    public float CurrentAmmo
    {
        get
        {
            return currentAmmo;
        }
    }

    public float MaxAmmo
    {
        get
        {
            return maxAmmo;
        }
    }
}
