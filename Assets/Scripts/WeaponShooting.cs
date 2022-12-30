using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    private Camera cam;
    private Inventory inventory;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        float tcurrentWeaponRange = inventory.GetItem(0).range;

        if(Physics.Raycast(ray, out hit, tcurrentWeaponRange))
        {
            Debug.Log(hit.transform.name);
            Debug.Log(tcurrentWeaponRange);
        }
    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
        inventory = GetComponent<Inventory>();
    }
}
