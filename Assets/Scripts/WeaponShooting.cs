using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    private float lastShootTime;
    public GameObject currentWeaponObject;
    private Transform currentWeaponBarrel;

    private Camera cam;
    private Inventory inventory;

    private void Start()
    {
        GetReferences();
        GetWeaponBarrel();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void GetWeaponBarrel()
    {
        currentWeaponBarrel = currentWeaponObject.transform.GetChild(0);
    }

    private void RaycastShoot(Weapon currentWeapon)
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        float tcurrentWeaponRange = currentWeapon.range;

        if(Physics.Raycast(ray, out hit, tcurrentWeaponRange))
        {
            Debug.Log(hit.transform.name);
        }
        Instantiate(currentWeapon.muzzleFlashParticles, currentWeaponBarrel);
    }

    private void Shoot()
    {
        Weapon currentWeapon = inventory.GetItem(0);

        if(Time.time > lastShootTime + currentWeapon.fireRate)
        {
            Debug.Log("Shoot");
            lastShootTime = Time.time;

            RaycastShoot(currentWeapon);
        }
    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
        inventory = GetComponent<Inventory>();
    }
}
