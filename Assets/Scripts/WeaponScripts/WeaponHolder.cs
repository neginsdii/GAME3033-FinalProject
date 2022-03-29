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
    [SerializeField]
    Transform GripSocketLocation;

    bool WasFiring = false;
    bool FiringPressed = false;
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        GameObject spawnWeapon = Instantiate(weaponToSpawn, WeaponSocket.transform.position, WeaponSocket.transform.rotation, WeaponSocket.transform);
        animator = GetComponent<Animator>();

        equippedWeapon = spawnWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialized(this);
          PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        GripSocketLocation = equippedWeapon.gripLoction;
    }


    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, GripSocketLocation.transform.position);
    }

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
        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        }
        animator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    public void StopFiring()
    {
        animator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();
    }
    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        StartReloading();
    }
    public void StartReloading()
    {
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
        if (animator.GetBool(isReloadingHash)) return;
        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        animator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));

    }
}