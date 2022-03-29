using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None, Pistol, AssualtRifle
}

public enum WeaponFiringPattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst
}
[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public int totalBullets;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool Repeating;
    public LayerMask weaponHitLayer;
    public WeaponFiringPattern WeaponFiringPattern;
}
public class WeaponComponent : MonoBehaviour
{
    public Transform gripLoction;
    public WeaponHolder WeaponHolder;

    [SerializeField]
    public WeaponStats weaponStats;
    [SerializeField]
    protected ParticleSystem firingEffect;
    public Transform ParticleLocation;

    public bool isFiring = false;
    public bool isReloading = false;

    protected Camera MainCamera;

    void Awake()
    {
        MainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Initialized(WeaponHolder _weaponHolder)
    {
        WeaponHolder = _weaponHolder;
    }

    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.Repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
        if (firingEffect)
        {
            firingEffect.Stop();
        }
    }
    protected virtual void FireWeapon()
    {
        Debug.Log("Firing weapon");
        weaponStats.bulletsInClip--;
    }
    public virtual void StartReloading()
    {
        isReloading = true;
        ReloeadWeapon();
    }
    public virtual void StopReloading()
    {
        isReloading = false;
        if (firingEffect)
        {
            firingEffect.Stop();
        }
    }
    protected virtual void ReloeadWeapon()
    {
        //if (firingEffect.isPlaying)
        //{
        //    firingEffect.Stop();
        //}
        int bulletsToReload = weaponStats.clipSize - weaponStats.totalBullets;
        if (bulletsToReload < 0)
        {
            weaponStats.bulletsInClip = weaponStats.clipSize;
            weaponStats.totalBullets -= weaponStats.clipSize;
        }
        else
        {
            weaponStats.bulletsInClip = weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
    }
}
