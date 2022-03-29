using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARM4Component : WeaponComponent
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void FireWeapon()
    {
        Vector3 hitLocation;
        if (weaponStats.bulletsInClip > 0 && !isReloading && !WeaponHolder.playerController.isRunning)
        {
            base.FireWeapon();
            if (firingEffect)
            {
                firingEffect.Play();
            }
            Ray screenRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayer))
            {
                hitLocation = hit.point;
                Vector3 hitDirection = hit.point - MainCamera.transform.position;
                Debug.DrawRay(MainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1f);
            }
            print("bullet count:" + weaponStats.bulletsInClip);
        }
        else if (weaponStats.bulletsInClip <= 0)
        {
            WeaponHolder.StartReloading();
        }
    }
}
