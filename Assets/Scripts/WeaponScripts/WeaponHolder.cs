using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    GameObject weaponToSpawn;
    public PlayerController playerController;
    Animator animator;
    Sprite crossHairImage;
    WeaponComponent equippedWeapon;
    [SerializeField]
    GameObject WeaponSocket;
    public WeaponComponent GetEquippedWeapon => equippedWeapon;

    [SerializeField]
    Transform GripSocketLocation;

    [SerializeField]
    GameObject weaponSocketLocation;
    bool WasFiring = false;
    bool FiringPressed = false;
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    [SerializeField]
    private WeaponScriptable startWeapon;
    GameObject spawnedWeapon;
    [SerializeField]
 //   private WeaponAmmoUI weaponAmmoUI;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        playerController.inventory.AddItem(startWeapon, 1);
        startWeapon.UseItem(playerController);
       // GameObject spawnWeapon = Instantiate(weaponToSpawn, WeaponSocket.transform.position, WeaponSocket.transform.rotation, WeaponSocket.transform);

      //  equippedWeapon = spawnWeapon.GetComponent<WeaponComponent>();
      //  equippedWeapon.Initialized(this);
       //   PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
     //   GripSocketLocation = equippedWeapon.gripLoction;
    }


    void Update()
    {

    }

    //private void OnAnimatorIK(int layerIndex)
    //{
    //    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
    //    animator.SetIKPosition(AvatarIKGoal.LeftHand, GripSocketLocation.transform.position);
    //}

    public void OnFire(InputValue value)
    {
        FiringPressed = value.isPressed;
        if (FiringPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }


    }


    public void StartFiring()
    {
        if (!equippedWeapon) return;

        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        };

        playerController.isFiring = true;
        animator.SetBool(isFiringHash, true);
        equippedWeapon.StartFiringWeapon();
    }

    public void StopFiring()
    {
        if (!equippedWeapon) return;

        playerController.isFiring = false;
        animator.SetBool(isFiringHash, false);
        equippedWeapon.StopFiringWeapon();
    }
    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        StartReloading();
    }
    public void StartReloading()
    {
        if (!equippedWeapon) return;

        if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInClip == equippedWeapon.weaponStats.clipSize) return;

        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0) return;
        animator.SetBool(isReloadingHash, true);
        equippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);


    }

    public void StopReloading()
    {
        if (!equippedWeapon) return;

        if (animator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        animator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));

    }

    public void EquipWeapon(WeaponScriptable weaponScriptable)
    {
        if (!weaponScriptable) return;

        spawnedWeapon = Instantiate(weaponScriptable.itemPrefab, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);

        if (!spawnedWeapon) return;

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();

        if (!equippedWeapon) return;
        Debug.Log("Weapon Stats: " + equippedWeapon.weaponStats.weaponName);

        equippedWeapon.Initialized(this, weaponScriptable);
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
    }

    public void UnquipWeapon()
    {
        if (!equippedWeapon) return;
        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
    }
}