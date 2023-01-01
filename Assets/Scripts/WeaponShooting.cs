using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    private float lastShootTime = 0;

    [SerializeField] private bool canShoot;
    [SerializeField] private bool canReload;

    [SerializeField] private int primaryCurrentAmmo;
    [SerializeField] private int primaryCurrentAmmoStorage;

    [SerializeField] private bool primaryMagazingIsEmpty = false;

    public GameObject currentWeaponObject;
    private Transform currentWeaponBarrel;

    private Camera cam;
    private Inventory inventory;
    private PlayerHUD hud;

    private void Start()
    {
        GetReferences();
        GetWeaponBarrel();
        InitAmmo();
        canShoot = true;
        canReload = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
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

        if (Physics.Raycast(ray, out hit, tcurrentWeaponRange))
        {
            Debug.Log(hit.transform.name);
        }
        Instantiate(currentWeapon.muzzleFlashParticles, currentWeaponBarrel);
    }

    private void Shoot()
    {
        CheckCanShoot();

        if(canShoot && canReload)
        {
            Weapon currentWeapon = inventory.GetItem(0);

            if (Time.time > lastShootTime + currentWeapon.fireRate)
            {
                Debug.Log("Shoot");
                lastShootTime = Time.time;

                RaycastShoot(currentWeapon);
                UseAmmo(1, 0);
            }

        }
        else
        {
            Debug.Log("Not enough ammo");
        }


    }

    private void UseAmmo(int currentAmmoUsed, int currentAmmoStoredUsed)
    {
        if (primaryCurrentAmmo <= 0)
        {
            primaryMagazingIsEmpty = true;
            CheckCanShoot();
        }
        else
        { 
            primaryCurrentAmmo -= currentAmmoUsed;
            primaryCurrentAmmoStorage -= currentAmmoStoredUsed;
            hud.UpdateWeaponAmmoUI(primaryCurrentAmmo, primaryCurrentAmmoStorage);
        }
    }

    private void AddAmmo(int currentAmmoAdded, int currentAmmoStoredAdded)
    {
        primaryCurrentAmmo += currentAmmoAdded;
        primaryCurrentAmmoStorage += currentAmmoStoredAdded;
        hud.UpdateWeaponAmmoUI(currentAmmoAdded, currentAmmoStoredAdded);
    }

    private void Reload()
    {
        if (canReload)
        {
            int ammoToReload = inventory.GetItem(0).magazineSize - primaryCurrentAmmo;

            if (primaryCurrentAmmoStorage >= ammoToReload)
            {


                if (primaryCurrentAmmo == inventory.GetItem(0).magazineSize)
                {
                    Debug.Log("ammo is Full");
                    return;
                }

                AddAmmo(ammoToReload, 0);
                UseAmmo(0, ammoToReload);

                primaryMagazingIsEmpty = false;
                CheckCanShoot();
            }
            else if (ammoToReload > 0 && primaryCurrentAmmoStorage > 0)
            {
                AddAmmo(primaryCurrentAmmoStorage, 0);
                UseAmmo(0, primaryCurrentAmmoStorage);

            }
            Debug.Log("Not enought ammo to reload");
        }
        else
            Debug.Log("Can't reload time");
        
        
    }

    private void CheckCanShoot()
    {
        if (primaryMagazingIsEmpty)
            canShoot = false;
        else
            canShoot = true;
    }

  

    private void InitAmmo()
    {
        primaryCurrentAmmo = inventory.weapons[0].magazineSize;
        primaryCurrentAmmoStorage = inventory.weapons[0].storedAmmo;

    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
        inventory = GetComponent<Inventory>();
        hud = GetComponent<PlayerHUD>();
    }
}
